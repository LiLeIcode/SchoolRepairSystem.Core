using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystemModels
{
    public class Users:BaseEntity
    {
        [StringLength(20,MinimumLength = 6)]
        [Required]
        public string UserName { get; set; }
        [StringLength(20, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
    }
}