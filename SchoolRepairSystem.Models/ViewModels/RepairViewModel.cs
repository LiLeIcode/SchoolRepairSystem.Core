using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models.ViewModels
{
    public class RepairViewModel
    {
        /// <summary>
        /// 层
        /// </summary>
        [Required]
        public string Layer { get; set; }
        /// <summary>
        /// 栋
        /// </summary>
        [Required]
        public string Tung { get; set; }
        /// <summary>
        /// 寝
        /// </summary>
        [Required]
        public string Dorm { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        public string Desc { get; set; }
    }
}