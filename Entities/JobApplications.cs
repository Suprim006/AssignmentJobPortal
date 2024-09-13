namespace AssignmentJobPortal.Entities
{
    public class JobApplications
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }
        public int JobId { get; set; }
        public Jobs? Job { get; set; }
        public string? CoverLetter { get; set; }
    }
}
