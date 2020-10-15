namespace SchoolRepairSystem.Models.ViewModels
{
    public class ResponseMessage<T>
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public bool Success { get; set; }
        public T ResponseInfo { get; set; }
    }
}