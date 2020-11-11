using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SchoolRepairSystem.Models
{
    public class ReportForRepair:BaseEntity
    {
        /// <summary>
        /// 报修人的id
        /// </summary>
        [Required]
        public long UserId { get; set; }
        /// <summary>
        /// 层
        /// </summary>
        [Required]
        [StringLength(10,MinimumLength = 1)]
        public string Layer { get; set; }
        /// <summary>
        /// 栋
        /// </summary>
        [Required]
        [StringLength(10,MinimumLength = 1)]
        public string Tung { get; set; }
        /// <summary>
        /// 寝室
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Dorm { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Desc { get; set; }

        /// <summary>
        /// 待处理状态,0 or null没人接受维修请求，1有人接受正在处理，2已经处理完成
        /// </summary>
        public int WaitHandle { get; set; }

        /// <summary>
        /// 评价，0 or null没有评论，1好评，2中评，3差评
        /// </summary>
        public int Evaluate { get; set; }
        /// <summary>
        /// 接受任务者的角色
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 接受任务者
        /// </summary>
        public long WorkerId { get; set; }
        


    }
}