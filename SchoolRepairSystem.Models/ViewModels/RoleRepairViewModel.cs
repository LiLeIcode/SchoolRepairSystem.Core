using System;
using System.Collections.Generic;

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

        public List<GoodsInfo> GoodsInfos { get; set; }
        public long  LoginUserId { get; set; }
        ///// <summary>
        ///// 取出的物品名
        ///// </summary>
        //public string Goods { get; set; }

        ///// <summary>
        ///// 取出的数量
        ///// </summary>
        //public long PickUp { get; set; }
        /// <summary>
        /// 取出者的名字
        /// </summary>
        public string UserName { get; set; }
    }
}