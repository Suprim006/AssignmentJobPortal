namespace AssignmentJobPortal.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string CompanyName { get; set; } // Company relation
        public string CategoryName { get; set; } // Category relation
    }

}
