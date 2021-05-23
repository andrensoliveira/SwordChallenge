using System.Collections.Generic;

namespace WebApi.DTO
{
    public class TaskPagingDTO
    {
        public long Count {get;set;}
        public List<TaskEntryDTO> Tasks {get;set;}
    }
}