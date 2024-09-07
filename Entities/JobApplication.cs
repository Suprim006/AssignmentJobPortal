namespace AssignmentJobPortal.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string CoverLetter { get; set; }
    }
}
