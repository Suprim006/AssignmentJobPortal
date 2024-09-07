namespace AssignmentJobPortal.Models
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } // User relation
        public int RoleId { get; set; }
        public string RoleName { get; set; } // Role relation
    }

}
