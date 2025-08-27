using IEEE.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace IEEE.DTO.UserDTO
{
    public class EditUserDto
    {
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public Faculty Faculty { get; set; }
        public StudyYear Year { get; set; }
        public Goverment Goverment { get; set; }
        public string PhoneNumber { get; set; }
        public Sex Sex { get; set; }
    }
}