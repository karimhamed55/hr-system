namespace IEEE.Entities
{
    public class Committee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int HeadId { get; set; }
        public User? Head { get; set; }
        public ICollection<Tasks>? Tasks { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
