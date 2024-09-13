using Microsoft.AspNetCore.Builder;

namespace AssignmentJobPortal.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public Roles? Role { get; set; }
        public List<JobApplications>? JobApplications { get; set; }
    }
}
