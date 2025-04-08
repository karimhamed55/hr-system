namespace IEEE.Entities

{
    public class Users_Tasks
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int TaskId { get; set; }

        public Tasks? Tasks { get; set; }
        public int Score { get; set; }

    }
}
