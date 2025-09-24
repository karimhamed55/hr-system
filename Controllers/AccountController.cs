using IEEE.Data;
using IEEE.DTO.UserDTO;
using IEEE.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IEEE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> roleManager;
        private readonly AppDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private readonly IConfiguration config;

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<User> UserManager, IConfiguration config, Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> _roleManager, AppDbContext context)
        {
            userManager = UserManager;
            this.config = config;
            roleManager = _roleManager;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // 1. تحقق من وجود الـ Role بالـ Id
                var role = await roleManager.FindByIdAsync(dto.RoleId.ToString());
                if (role == null)
                    return BadRequest(new { message = $"Role with ID '{dto.RoleId}' does not exist." });

                // نخزن الاسم جوه الـ DTO عشان نستخدمه مع Identity
                dto.RoleName = role.Name;

                // 2. منع تكرار الإيميل
                var existingUser = await userManager.FindByEmailAsync(dto.Email);
                if (existingUser != null)
                    return BadRequest(new { message = "Email already exists." });

                // 3. إنشاء كيان الـ User
                var user = new User
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FName = dto.FirstName,
                    MName = dto.MiddleName,
                    LName = dto.LastName,
                    Faculty = dto.Faculty,
                    PhoneNumber = dto.Phone,
                    Sex = dto.Sex,
                    Goverment = dto.Goverment,
                    Year = dto.Year,
                    IsActive = false,
                    EmailConfirmed = false,
                    RoleId = dto.RoleId // نخزن الـ RoleId في جدول Users
                };

                // 4. إنشاء المستخدم في Identity
                var createResult = await userManager.CreateAsync(user, dto.Password);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToArray();
                    return BadRequest(new { step = "CreateUser", errors });
                }

                // 5. إضافة الـ Role باستخدام الاسم
                var addRoleResult = await userManager.AddToRoleAsync(user, dto.RoleName);
                if (!addRoleResult.Succeeded)
                {
                    await userManager.DeleteAsync(user); // Cleanup لو فشل
                    var errors = addRoleResult.Errors.Select(e => e.Description).ToArray();
                    return BadRequest(new { step = "AddToRole", errors });
                }

                // 6. جلب المستخدم من الـ DbContext لتحديث الـ Committees
                var userFromDb = await _context.Users
                    .Include(u => u.Committees)
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if (userFromDb == null)
                    return StatusCode(500, new { message = "User created but could not be reloaded." });

                // 7. إضافة Committees (لو فيه)
                if (dto.CommitteeIds != null && dto.CommitteeIds.Any())
                {
                    var committees = await _context.Committees
                        .Where(c => dto.CommitteeIds.Contains(c.Id))
                        .ToListAsync();

                    foreach (var c in committees)
                    {
                        if (!userFromDb.Committees.Any(x => x.Id == c.Id))
                            userFromDb.Committees.Add(c);
                    }

                    await _context.SaveChangesAsync();
                }

                // 8. جلب الـ Roles الفعلية للمستخدم (Debug أو للتأكيد)
                var actualRoles = await userManager.GetRolesAsync(user);

                return Ok(new
                {
                    message = "User created successfully",
                    userId = user.Id,
                    roleName = dto.RoleName,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the user.",
                    error = ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }



        [HttpPost("Login")]
                    public async Task<IActionResult> Login(LoginDto userFromRequest)
                    {
                        if (ModelState.IsValid)
                        {
                            User userfromdb = await userManager.FindByNameAsync(userFromRequest.Email);

                            if (userfromdb != null)
                            {
                                bool found = await userManager.CheckPasswordAsync(userfromdb, userFromRequest.Password);
                                if (found)
                                {
                                    // الحصول على Roles الخاصة بالمستخدم باستخدام UserManager
                                    var userRoles = await userManager.GetRolesAsync(userfromdb);

                                    List<Claim> UserClaim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userfromdb.Id.ToString()),
                    new Claim(ClaimTypes.Name, userfromdb.Email),
                    new Claim("IsActive", userfromdb.IsActive.ToString()),
                    new Claim("RoleId", userfromdb.RoleId.ToString())
                };

                                    // إضافة جميع الـ Roles كـ Claims
                                    foreach (var roleName in userRoles)
                                    {
                                        UserClaim.Add(new Claim(ClaimTypes.Role, roleName));
                                    }

                                    // الحصول على اسم الـ Role الأساسي
                                    var primaryRoleName = userRoles.FirstOrDefault();

                                    JwtSecurityToken mytoken = new JwtSecurityToken(
                                        issuer: config["Jwt:IssuerIP"],
                                        audience: config["Jwt:AudienceIP"],
                                        expires: DateTime.Now.AddHours(1),
                                        claims: UserClaim,
                                        signingCredentials: new SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KiraSuperUltraMegaSecretKey!1234567890")),
                                            SecurityAlgorithms.HmacSha256
                                        )
                                    );

                                    var tokenString = new JwtSecurityTokenHandler().WriteToken(mytoken);

                                    return Ok(new
                                    {
                                        token = tokenString,
                                        user = new
                                        {
                                            id = userfromdb.Id,
                                            firstName = userfromdb.FName,
                                            middleName = userfromdb.MName,
                                            lastName = userfromdb.LName,
                                            email = userfromdb.Email,
                                            phone = userfromdb.PhoneNumber,
                                            sex = userfromdb.Sex,
                                            goverment = userfromdb.Goverment,
                                            year = userfromdb.Year,
                                            faculty = userfromdb.Faculty,
                                            roleId = userfromdb.RoleId,
                                            roleName = primaryRoleName,
                                            roles = userRoles // إضافة جميع الـ Roles
                                        }
                                    });
                                }
                            }

                            ModelState.AddModelError("Username", "Username OR Password Invalid");
                            return Unauthorized(ModelState);
                        }

                        return BadRequest(ModelState);
                    }

                }
            }
            