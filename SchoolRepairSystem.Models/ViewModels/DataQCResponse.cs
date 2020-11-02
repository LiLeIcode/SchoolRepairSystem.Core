namespace SchoolRepairSystem.Models.ViewModels
{
    public class DataQCResponse<T>
    {
        public int Count { get; set; }
        public int RepairsWaitHandle0 { get; set; }
        public int RepairsWaitHandle1 { get; set; }
        public int RepairsWaitHandle2 { get; set; }
        public T ArrayT { get; set; }
    }
}