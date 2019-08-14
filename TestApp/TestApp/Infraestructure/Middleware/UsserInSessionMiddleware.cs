using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestApp.Core.Infraestructure;

namespace TestApp.Infraestructure.Middleware
{
    public class UserInSessionMiddleware
    {
        private RequestDelegate _next;

        public UserInSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var contextUser = context.User;
            var userId = contextUser.FindFirst(s => s.Type == "sub")?.Value;
            if(userId == null)
                userId = contextUser.FindFirst(s => s.Type == "UserId")?.Value;

            var userInSession = context.RequestServices.GetRequiredService<IUserInSession>();
            userInSession.UserId = userId;

            await _next(context);
        }
    }
}
