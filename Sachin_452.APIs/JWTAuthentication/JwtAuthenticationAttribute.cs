
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Sachin_452.APIs.JWTAuthentication
{
    public class JwtAuthenticationAttribute :  ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)

        {
            var request = actionContext.Request;

            var authHeader = request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization header is missing or invalid.");
                return;
            }

            var token = authHeader.Parameter;
            var userName = Authentication.ValidateToken(token);
            if (userName == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}