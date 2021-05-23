using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTO;
using WebApi.Repositories.Interface;

namespace WebApi.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SwordChallengeContext _context;

        public TaskRepository(SwordChallengeContext context)
        {
            _context = context;
        }

        public async Task<dynamic> GetManagerTasks(int pageIndex, int pageSize)
        {
            IQueryable<Models.Task> query = _context.Set<Models.Task>().Where(x => !x.Deleted);

            dynamic item = new ExpandoObject();
            item.Count = await query.CountAsync();
            item.Tasks = await query.Select(x => new
            {
                TaskId = x.TaskId,
                ApplicationUserId = x.ApplicationUserId,
                Summary = x.Summary,
                CreatedAt = x.CreatedAt,
                PerformedDate = x.PerformedDate
            }).Skip(pageIndex * pageSize).Take(pageSize).AsQueryable().ToListAsync();

            return item;
        }

        public async Task<dynamic> GetTechnicianTasks(int pageIndex, int pageSize, long userId)
        {
            IQueryable<Models.Task> query = _context.Set<Models.Task>().Where(x => x.ApplicationUserId == userId && !x.Deleted);

            dynamic item = new ExpandoObject();
            item.Count = await query.CountAsync();
            item.Tasks = await query.Select(x => new
            {
                TaskId = x.TaskId,
                ApplicationUserId = x.ApplicationUserId,
                Summary = x.Summary,
                CreatedAt = x.CreatedAt,
                PerformedDate = x.PerformedDate
            }).Skip(pageIndex * pageSize).Take(pageSize).AsQueryable().ToListAsync();

            return item;
        }

        public async Task Post(Models.Task entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            await _context.Set<Models.Task>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Task> Update(TaskRequestDTO dto)
        {
            var task = await _context.Set<Models.Task>().FirstOrDefaultAsync(x => x.TaskId == dto.TaskId);

            if (task != null && !task.Deleted)
            {
                var now = DateTime.UtcNow;
                task.UpdatedAt = now;
                task.UpdateUser = dto.CreateUser;
                if(dto.Summary != task.Summary) task.Summary = dto.Summary;
                if(dto.Completed == true) task.PerformedDate = now;

                var res = _context.Set<Models.Task>().Update(task);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            
            return null;
        }

        public async Task Delete(long id)
        {
            var task = await _context.Set<Models.Task>().FirstOrDefaultAsync(x => x.TaskId == id);

            if (task != null && !task.Deleted)
            {
                task.Deleted = true;
                task.DeletedAt = DateTime.UtcNow;
                task.DeleteUser = task.CreateUser;

                _context.Set<Models.Task>().Update(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}