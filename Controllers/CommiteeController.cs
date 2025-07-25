using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IEEE.Data;
using IEEE.Entities;
using IEEE.DTO.CommitteeDto;
using IEEE.DTO.MeetingsDto;

namespace IEEE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitteesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommitteesController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommitteeGetDto>>> GetCommittees()
        {
            var committees = await _context.Committees.ToListAsync();

            var committeesDto = committees.Select(c => new CommitteeGetDto
            {
                Id = c.Id,
                Name = c.Name,
                HeadId = c.HeadId ?? 0
            });
            return Ok(committeesDto);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CommitteeGetDto>> GetCommittee(int id)
        {
            var committee = await _context.Committees.FindAsync(id);

            if (committee == null)
            {
                return NotFound();
            }
            var committeeDto = new CommitteeGetDto
            {
                Id = committee.Id,
                Name = committee.Name,
                HeadId = committee.HeadId ?? 0

            };

            return Ok(committeeDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCommittee(CommitteeCreateDto committeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var committee = new Committee
            {
                Name = committeeDto.Name,
                HeadId = committeeDto.HeadId ?? null,
            };
            await _context.Committees.AddAsync(committee);
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommittee(int id,CommitteeUpdateDto committeeUpdateDto)
        {
            var committee = await _context.Committees.FindAsync(id);
            if(committee == null)
            {
                return NotFound();
            }
            committee.Name = committeeUpdateDto.Name;
            committee.HeadId = committeeUpdateDto.HeadId ?? committee.HeadId;

      
                await _context.SaveChangesAsync();
       
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommittee(int id)
        {
            var committee = await _context.Committees.FindAsync(id);
            if (committee == null)
            {
                return NotFound();
            }

            _context.Committees.Remove(committee);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
