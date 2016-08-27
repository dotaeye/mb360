using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public partial class UserRole : BaseEntity
    {
        public string Name { get; set; }

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

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
