using Microsoft.AspNetCore.Builder;

namespace AssignmentJobPortal.Entities
{
    public class Jobs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public int CompanyId { get; set; }
        public Companies? Company { get; set; }
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }
        public List<JobApplications>? JobApplications { get; set; }
    }
}
