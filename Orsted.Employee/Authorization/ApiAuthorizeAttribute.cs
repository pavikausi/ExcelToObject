using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Text;
using Orsted.Employee.Models;

namespace Orsted.Employee.Authorization
{
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            if (AuthenticationHeaderValue.TryParse(actionContext.HttpContext.Request.Headers["Authorization"], out var authHeader))
            {
                // Gets header parameters  
                string authenticationString = authHeader.Parameter;
                string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                // Gets username and password  
                string usrename = originalString.Split(':')[0];
                string password = originalString.Split(':')[1];

                // Validate username and password  
                if (!VaidateUser(usrename, password, actionContext))
                {
                    // returns unauthorized error  
                    actionContext.Result = new UnauthorizedResult();
                }
            }
            else
            {
                actionContext.Result = new UnauthorizedResult();

            }
        }
        public static bool VaidateUser(string username, string password, AuthorizationFilterContext actionContext)
        {
            var options = actionContext.HttpContext.RequestServices.GetService(typeof(IOptions<ApiOptions>)) as IOptions<ApiOptions>;

            if (username == options.Value.ApiUserName && password == options.Value.ApiPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
