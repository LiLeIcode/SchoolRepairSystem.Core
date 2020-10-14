namespace SchoolRepairSystemModels.ViewModels
{
    public class RoleReportForRepairViewModel
    {
        public string UserName { get; set; }
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
    }
}