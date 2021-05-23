using WebApi.Data;
using WebApi.Models;
using WebApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace WebApi.Repositories.Implementation
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly SwordChallengeContext _context;

        public ApplicationUserRepository(SwordChallengeContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Get(string userName, string pw)
        {
            return await _context.Set<ApplicationUser>().Include(x => x.Role).FirstOrDefaultAsync(x => x.UserName == userName && x.Password == pw);
        }

        public async Task<ApplicationUser> Get(long id)
        {
            return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.ApplicationUserId == id);
        }

        public async Task<List<ApplicationUser>> GetEmployeeList()
        {
            return await _context.Set<ApplicationUser>().Where(x => x.RoleId == (long)Constants.RolesName.Technician && !x.Deleted).ToListAsync();
        }

        public async Task<bool> IsManager(long id)
        {
            return await _context.Set<ApplicationUser>().AnyAsync(x => x.ApplicationUserId == id && x.RoleId == (long)Constants.RolesName.Manager);
        }
    }
}