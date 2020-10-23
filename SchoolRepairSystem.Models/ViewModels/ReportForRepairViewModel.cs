using System;

namespace SchoolRepairSystem.Models.ViewModels
{
    public class ReportForRepairViewModel
    {
        public long Id { get; set; }
        /// <summary>
        /// 报修者名字
        /// </summary>
        //public string UserName { get; set; }
        /// <summary>
        /// 层
        /// </summary>
        public string Layer { get; set; }
        /// <summary>
        /// 栋
        /// </summary>
        public string Tung { get; set; }
        /// <summary>
        /// 寝室
        /// </summary>
        public string Dorm { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 待处理状态,0 or null没人接受维修请求，1有人接受正在处理，2已经处理完成
        /// </summary>
        public int WaitHandle { get; set; }

        /// <summary>
        /// 评价，0 or null没有评论，1好评，2中评，3差评
        /// </summary>
        public int Evaluate { get; set; }

        public DateTime DateTime { get; set; }
    }
}