namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(ApplicationUser user, string token)
        {
            Id = user.ApplicationUserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Role = user.Role.RoleName;
            Token = token;
        }
    }
}