using System;

namespace SchoolRepairSystem.Models.ViewModels
{
    public class RoleRepairViewModel: ReportForRepairViewModel
    {
        /// <summary>
        /// 角色的id
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 工人的id
        /// </summary>
        public long WorkerId { get; set; }
        /// <summary>
        /// 登录者的id
        /// </summary>
        public long  LoginUserId { get; set; }
    }
}