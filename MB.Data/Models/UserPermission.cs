

using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public partial class UserPermission : BaseEntity
    {
        public string Name { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }

        public string Group { get; set; }

        public bool IsApi { get; set; }

        [DTO(false, true)]
        public string CreateUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [DTO(false, true)]
        public string LastUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> LastTime { get; set; }

        [DTO(false, true)]
        public bool Deleted { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
