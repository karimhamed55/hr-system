namespace IEEE.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string City { get; set; }

        public int? CommitteeId { get; set; }
        public Committee? Committee { get; set; }

        public ICollection<Role>? Roles { get; set; } = new List<Role>();
        public ICollection<Tasks>? Tasks { get; set; } = new List<Tasks>();





    }
}
