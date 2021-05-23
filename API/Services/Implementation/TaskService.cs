using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using WebApi.DTO;
using WebApi.Repositories.Interface;
using WebApi.Services.Interface;

namespace WebApi.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repo, INotificationService notificationService, IUserService userService, IMapper mapper)
        {
            _repo = repo;
            _notificationService = notificationService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<TaskPagingDTO> GetTasks(int pageIndex, int pageSize, long userId)
        {
            var manager = await _userService.IsManager(userId);

            var obj = manager == true ? await _repo.GetManagerTasks(pageIndex, pageSize) : await _repo.GetTechnicianTasks(pageIndex, pageSize, userId);
            var count = JsonConvert.DeserializeObject<long>(JsonConvert.SerializeObject(obj.Count));
            var tasks = JsonConvert.DeserializeObject<List<Models.Task>>(JsonConvert.SerializeObject(obj.Tasks));

            var list = _mapper.Map<List<Models.Task>, List<TaskEntryDTO>>(tasks);

            return new TaskPagingDTO { Count = count, Tasks = list };
        }

        public async Task PostTask(TaskRequestDTO dto)
        {
            await _repo.Post(_mapper.Map<TaskRequestDTO, Models.Task>(dto));
        }

        public async Task PutTask(TaskRequestDTO dto)
        {
            var task = await _repo.Update(dto);

            if (dto.Completed == true)
                await _notificationService.PostNotification(dto, task.PerformedDate);
        }

        public async Task DeleteTask(long id)
        {
            await _repo.Delete(id);
        }
    }
}