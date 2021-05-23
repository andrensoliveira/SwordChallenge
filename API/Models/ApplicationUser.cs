namespace WebApi.Models
{
    public partial class ApplicationUser
    {
        public long ApplicationUserId { get; set; }
        public long RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }

        public virtual Role Role { get; set; }
    }
}
