using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models.ViewModels
{
    public class UserRoleViewModel
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long RoleId { get; set; }
    }
}