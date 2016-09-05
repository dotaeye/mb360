using System;
using System.Collections.Generic;
using SQ.Core.Data;
using SQ.Core.DTO;

namespace MB.Data.Models
{
    [DTOIgnore]
    public partial class UserActivities : BaseEntity
    {
        public Nullable<System.DateTime> Time { get; set; }
        public string UserId { get; set; }

        [DTO(false, true)]
        public bool Deleted { get; set; }

        public UserActivityType Type { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
