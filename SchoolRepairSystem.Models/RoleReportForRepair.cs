using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystem.Models
{
    public class RoleReportForRepair:BaseEntity
    {
        /// <summary>
        /// 报修信息的id
        /// </summary>
        [Required]
        public long RepairId { get; set; }
        /// <summary>
        /// 工人角色id
        /// </summary>
        [Required]
        public long RoleId { get; set; }

    }
}