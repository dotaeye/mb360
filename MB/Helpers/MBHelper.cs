using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace MB.Helpers
{

    public enum RoleType
    {
        Admin = 1,
        Employee = 2,
        Member = 3,
        Normal = 4
    }

    public enum StorageType
    {
        System = 0,
        Member = 1
    }

    public class MBHelper
    {
        public static int GetUserRoleId(IPrincipal User)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Int32 userRoleId = 0;
            int.TryParse(claims.First(c => c.Type == "userRoleId").Value, out userRoleId);
            return userRoleId;
        }
    }
}