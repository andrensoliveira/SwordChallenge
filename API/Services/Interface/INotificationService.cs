using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTO;

namespace WebApi.Services.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetNotifications();
        Task PostNotification(TaskRequestDTO dto, System.DateTime? performedDate);
        Task UpdateRead(long id);
    }
}