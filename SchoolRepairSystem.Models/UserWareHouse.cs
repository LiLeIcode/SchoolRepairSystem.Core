using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models
{
    /// <summary>
    /// 先操作warehouse表再操作此表
    /// </summary>
    public class UserWareHouse:BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long GoodsId { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 1)]
        public string Goods { get; set; }

        /// <summary>
        /// 进货
        /// </summary>

        public int Purchase { get; set; } = 0;

        /// <summary>
        /// 取货
        /// </summary>
        public int PickUp { get; set; } = 0;

        /// <summary>
        /// 进出货id
        /// </summary>

        public long UserWareHouseId { get; set; }

    }
}