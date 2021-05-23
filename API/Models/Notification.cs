using System;

namespace WebApi.Models
{
   public partial class Notification
    {
        public long NotificationId { get; set; }
        public long TaskId { get; set; }
        public string Message { get; set; }
        public bool? IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreateUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeleteUser { get; set; }
        public bool Deleted { get; set; }
    }
}
