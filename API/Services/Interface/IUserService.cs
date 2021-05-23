using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Services.Interface
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<UserDTO> GetById(long id);
        Task<List<UserDTO>> GetEmployees();
        Task<bool> IsManager(long userId);
    }
}