using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models
{
    public class WareHouse:BaseEntity
    {
        [Required]
        [StringLength(20,MinimumLength = 1)]
        public string Goods { get; set; }
        [Required]
        public int Number { get; set; }

    }
}