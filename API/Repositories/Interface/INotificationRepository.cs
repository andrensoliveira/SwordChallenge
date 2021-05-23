using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories.Interface
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotifications();
        System.Threading.Tasks.Task Post(Notification entity);
        System.Threading.Tasks.Task UpdateRead(long id);
    }
}