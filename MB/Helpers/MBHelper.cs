using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using MB.Data.Models;
using System.Web;
using SQ.Core;

namespace MB.Helpers
{

    public enum StorageType
    {
        System = 0,
        Member = 1
    }

    public class MBHelper
    {



        public const string SPECS_FILTER_MODEL_KEY = "Mb.pres.filter.specs-{0}";
        public const string SPECS_FILTER_PATTERN_KEY = "Mb.pres.filter.specs";


        protected virtual Boolean IsRequestAvailable(HttpContext httpContext)
        {
            if (httpContext == null)
                return false;

            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (HttpException)
            {
                return false;
            }

            return true;
        }

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

        /// <summary>
        /// Remove query string from url
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryString">Query string to remove</param>
        /// <returns>New url</returns>
        public static string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            queryString = queryString.ToLowerInvariant();


            string str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryString))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    dictionary.Remove(queryString);

                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }

        /// <summary>
        /// Gets query string value by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static T QueryString<T>(string name)
        {
            string queryParam = null;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[name] != null)
                queryParam = HttpContext.Current.Request.QueryString[name];

            if (!String.IsNullOrEmpty(queryParam))
                return CommonHelper.To<T>(queryParam);

            return default(T);
        }
    }
}