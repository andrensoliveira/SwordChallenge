using System;

namespace WebApi.DTO{

    public class TaskEntryDTO
    {
        public long TaskId { get; set; }
        public long User { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PerformedDate { get; set; }
    }
}