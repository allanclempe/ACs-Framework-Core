using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace ACs.Security.Jwt
{
    public class JwtMiddleware500to401Error
    {
        RequestDelegate _next;

        public JwtMiddleware500to401Error(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = 401;
            }

        }
    }
}

