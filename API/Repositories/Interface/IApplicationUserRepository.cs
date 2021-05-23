using WebApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Repositories.Interface
{
    public interface IApplicationUserRepository
	{
		Task<ApplicationUser> Get(string userName, string pw);
		Task<ApplicationUser> Get(long id);
        Task<bool> IsManager(long id);
        Task<List<ApplicationUser>> GetEmployeeList();
    }
}