using System.Threading.Tasks;
using WebApi.DTO;

namespace WebApi.Services.Interface
{
    public interface ITaskService
    {
        Task<TaskPagingDTO> GetTasks(int pageIndex, int pageSize, long userId);

        Task PostTask(TaskRequestDTO dto);
        Task PutTask(TaskRequestDTO dto);
        Task DeleteTask(long id);
    }
}