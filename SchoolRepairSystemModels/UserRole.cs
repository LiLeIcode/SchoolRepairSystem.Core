using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystemModels
{
    public class UserRole:BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long RoleId { get; set; }

    }
}