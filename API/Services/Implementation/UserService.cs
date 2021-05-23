using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories.Interface;
using WebApi.Services.Interface;

namespace WebApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IApplicationUserRepository _repo;

        public UserService(IOptions<AppSettings> appSettings, IApplicationUserRepository repo, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _repo.Get(model.Username, model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<UserDTO> GetById(long id)
        {
            return _mapper.Map<ApplicationUser, UserDTO>(await _repo.Get(id));
        }

        public async Task<List<UserDTO>> GetEmployees()
        {
            return _mapper.Map<List<ApplicationUser>, List<UserDTO>>(await _repo.GetEmployeeList());
        }

        public async Task<bool> IsManager(long userId)
        {
            return await _repo.IsManager(userId);
        }

        // helper methods

        private string generateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 30 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ApplicationUserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}