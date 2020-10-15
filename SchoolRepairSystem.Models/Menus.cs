using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models
{
    public class Menus:BaseEntity
    {
        [Required,MaxLength(14)]
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单等级
        /// </summary>
        [Required]
        public int Grade { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Required] 
        public string Path { get; set; }


    }
}