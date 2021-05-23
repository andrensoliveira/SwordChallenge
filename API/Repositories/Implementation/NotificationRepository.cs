using WebApi.Models;
using WebApi.Repositories.Interface;
using System.Threading.Tasks;
using System;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace WebApi.Repositories.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SwordChallengeContext _context;

        public NotificationRepository(SwordChallengeContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetNotifications()
        {
            return await _context.Set<Notification>().Where(x => !x.Deleted && (bool)!x.IsRead).Take(5).ToListAsync();
        }

        public async System.Threading.Tasks.Task Post(Notification entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            await _context.Set<Notification>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateRead(long id)
        {
            var entity = await _context.Set<Notification>().FirstOrDefaultAsync(x => x.NotificationId == id);

            if (entity != null && !entity.Deleted)
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.IsRead = true;

                _context.Set<Notification>().Update(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}