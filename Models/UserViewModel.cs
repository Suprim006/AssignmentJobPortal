using System.ComponentModel.DataAnnotations;
using AssignmentJobPortal.Entities;

namespace AssignmentJobPortal.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        // Remove the Role property
        // public Roles Role { get; set; }
    }
}