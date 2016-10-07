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


        public static string GetCode(int number, int length, bool prepend = true)
        {
            var needZeroNum = 0;
            var code = string.Empty;
            if (number.ToString().Length >= length)
            {
                return number.ToString();
            }
            else
            {
                code = number.ToString();
                needZeroNum = length - number.ToString().Length;
                for (var i = 0; i < needZeroNum; i++)
                {
                    if (prepend)
                    {
                        code = "0" + code;
                    }
                    else
                    {
                        code += "0";
                    }
                }
                return code;
            }
        }
    }
}