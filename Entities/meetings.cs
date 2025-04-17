namespace IEEE.Entities
{
    public class meetings
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int CommitteeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Recap { get; set; }

        public User Creator { get; set; }
        public Committee Committee { get; set; }
    }
}
