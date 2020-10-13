using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystemModels
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