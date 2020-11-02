namespace SchoolRepairSystem.Models.ViewModels
{
    public class UserAddRoleViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public long RoleId { get; set; }
    }
}