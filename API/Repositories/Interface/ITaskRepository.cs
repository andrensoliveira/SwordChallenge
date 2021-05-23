using System.Threading.Tasks;
using WebApi.DTO;

namespace WebApi.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<dynamic> GetManagerTasks(int pageIndex, int pageSize);
        Task<dynamic> GetTechnicianTasks(int pageIndex, int pageSize, long userId);
        Task Post(Models.Task entity);
        Task<Models.Task> Update(TaskRequestDTO dto);
        Task Delete(long id);
    }
}