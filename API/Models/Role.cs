using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Role
    {
        public Role()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }

        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
