using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class Role
    {
        public Role()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public decimal RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
