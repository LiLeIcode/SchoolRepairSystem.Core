using System;

namespace SchoolRepairSystem.Models.ViewModels
{
    public class GoodsInfoViewModel
    {
        public long Id { get; set; }
        public string Goods { get; set; }
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
    }
}