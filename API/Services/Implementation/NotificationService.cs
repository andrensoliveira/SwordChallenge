using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Repositories.Interface;
using WebApi.Services.Interface;

namespace WebApi.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository repo, IUserService userService, IMapper mapper)
        {
            _repo = repo;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<NotificationDTO>> GetNotifications()
        {
            return _mapper.Map<List<Notification>, List<NotificationDTO>>(await _repo.GetNotifications());
        }

        public async System.Threading.Tasks.Task PostNotification(TaskRequestDTO dto, System.DateTime? performedDate)
        {
            var employee = await _userService.GetById(dto.ApplicationUserId);
            var message = string.Concat("The tech ", employee.FirstName, " ", employee.LastName," performed task ",dto.TaskId," at ", performedDate);

            await _repo.Post(new Notification() { TaskId = dto.TaskId, Message = message});
        }

        public async System.Threading.Tasks.Task UpdateRead(long id)
        {
            await _repo.UpdateRead(id);
        }
    }
}