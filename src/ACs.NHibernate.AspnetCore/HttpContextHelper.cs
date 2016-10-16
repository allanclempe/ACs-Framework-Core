using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ACs.NHibernate.AspnetCore
{
    public static class HttpContextHelper
    {

        private static IHttpContextAccessor _httpContextAccessor;
        public static IApplicationBuilder UseStaticContext(this IApplicationBuilder builder)
        {
            UseStaticContext((IHttpContextAccessor)builder.ApplicationServices.GetService(typeof(IHttpContextAccessor)));
            return builder;

        }
        public static void UseStaticContext(IHttpContextAccessor builder)
        {
            _httpContextAccessor = builder;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
    }
}

