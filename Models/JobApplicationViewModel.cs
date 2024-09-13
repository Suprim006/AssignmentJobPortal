namespace AssignmentJobPortal.Models
{
    public class JobApplicationViewModel
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; } // for displaying User name
        public int JobId { get; set; }
        public string? JobTitle { get; set; } // for displaying Job title
        public string? CoverLetter { get; set; }
    }
}
