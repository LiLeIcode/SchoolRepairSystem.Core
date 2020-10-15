using System;

namespace SchoolRepairSystem.Models.ViewModels
{
    public class UserWareHouseViewModel
    {
        public string UserName { get; set; }
        public string Goods { get; set; }
        /// <summary>
        /// 进货
        /// </summary>
        public int Purchase { get; set; }
        /// <summary>
        /// 取货
        /// </summary>
        public int PickUp { get; set; }

        public DateTime DateTime { get; set; }
    }
}