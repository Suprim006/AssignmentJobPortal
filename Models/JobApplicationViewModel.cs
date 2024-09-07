namespace AssignmentJobPortal.Models
{
    public class JobApplicationViewModel
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; } // Job relation
        public int UserId { get; set; }
        public string ApplicantName { get; set; } // User relation
        public string ApplicantEmail { get; set; }
        public DateTime AppliedDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected, etc.
    }

}
