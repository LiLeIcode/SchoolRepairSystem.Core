namespace SchoolRepairSystem.Models.ViewModels
{
    public class MenuViewModel
    {
        public long Id { get; set; }
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单等级
        /// </summary>
        public int Grade { get; set; }
        public string Path { get; set; }
    }
}