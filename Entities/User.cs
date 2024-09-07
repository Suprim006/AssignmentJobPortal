using Microsoft.AspNetCore.Builder;

namespace AssignmentJobPortal.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<JobApplication> JobApplications { get; set; }
    }
}
