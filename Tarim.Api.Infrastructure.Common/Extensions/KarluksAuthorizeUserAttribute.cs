using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Karluks.API.Infrastructure.Common.Extensions
{
    public class KarluksAuthorizeUserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {    
        /// <summary>
        /// Gets or sets admin profile
        /// </summary>
        public string[] Roles { get; set; }

    /// <inheritdoc />
    /// <summary>
    /// On Authorization method
    /// </summary>
    /// <param name="actionContext">Http action context</param>
    /// <param name="cancellationToken">Cancellation token object</param>
    /// <returns>false return unauthorized true will process</returns>
    public void OnAuthorization(AuthorizationFilterContext actionContext)
        {

        if (actionContext.HttpContext.User is ClaimsPrincipal myIntelsatPrincipal && myIntelsatPrincipal.Identity.IsAuthenticated &&
            this.Roles.Any(myIntelsatPrincipal.IsInRole))
        {
                return;
        }

            actionContext.Result = new UnauthorizedResult();
            return;
       // actionContext.HttpContext.Response = actionContext.HttpContext..CreateResponse(HttpStatusCode.Unauthorized);
       // return Task.FromResult<object>(null);
    }
}
}