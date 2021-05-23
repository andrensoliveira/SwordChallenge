using System;

namespace WebApi.Models
{
    public partial class Task
    {
        public long TaskId { get; set; }
        public long ApplicationUserId { get; set; }
        public string Summary { get; set; }
        public DateTime? PerformedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreateUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeleteUser { get; set; }
        public bool Deleted { get; set; }
    }
}
