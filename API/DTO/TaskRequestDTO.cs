using System;

namespace WebApi.DTO{

    public class TaskRequestDTO
    {
        public long TaskId { get; set; }
        public long ApplicationUserId { get; set; }
        public string Summary { get; set; }
        public long CreateUser { get; set; }
        public bool Completed { get; set; }
    }
}