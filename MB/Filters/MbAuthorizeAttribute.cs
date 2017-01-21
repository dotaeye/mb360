using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading.Tasks;

namespace MB.Filters
{
    public class MbAuthorizeAttribute : AuthorizationFilterAttribute
    {

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                return Task.FromResult<object>(null);
            }

            var userName = principal.FindFirst(ClaimTypes.Name).Value;
            int userRoleId;
            int.TryParse(principal.FindFirst("userRoleId").Value, out userRoleId);

            if (userRoleId < 3)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "没有足够权限访问");
                return Task.FromResult<object>(null);
            }

            //User is Authorized, complete execution
            return base.OnAuthorizationAsync(actionContext, cancellationToken);

        }
    }
}