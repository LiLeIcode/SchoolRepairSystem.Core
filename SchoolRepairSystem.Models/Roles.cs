using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models
{
    public class Roles:BaseEntity
    {
        [Required]
        [StringLength(15,MinimumLength = 2)]
        public string RoleName { get; set; }
        
    }
}