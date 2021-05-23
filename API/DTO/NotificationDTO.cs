namespace WebApi.DTO
{
    public class NotificationDTO
    {
        public long NotificationId { get; set; }
        public long TaskId { get; set; }
        public string Message { get; set; }
    }
}