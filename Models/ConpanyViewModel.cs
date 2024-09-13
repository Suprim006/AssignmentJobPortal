namespace AssignmentJobPortal.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int JobCount { get; set; } // Derived from number of jobs
    }
}
