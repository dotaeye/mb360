using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Data.DTO
{
    public class MemberDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string RefPhone { get; set; }

        public string Password { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool EmailConfirmed { get; set; }

        public Nullable<System.DateTime> LastLoginTime { get; set; }

        public Nullable<System.DateTime> CreateTime { get; set; }

        public string Avatar { get; set; }

        public bool Sex { get; set; }

        public string PhonePrefix { get; set; }

        public int UserRoleId { get; set; }

        public int JobId { get; set; }
    }
}
