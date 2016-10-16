using System;
using Microsoft.AspNetCore.Http;

namespace ACs.Framework.Web
{
    public static class HttpContextExtensions
    {
        public static Uri GetAbsoluteUri(this HttpRequest context, string relativePath = null)
        {
            var absoluteUri = new Uri(string.Concat(context.Scheme, "://", context.Host.ToUriComponent(), context.PathBase.ToUriComponent()));

            if (string.IsNullOrEmpty(relativePath))
                return absoluteUri;

            return new Uri(absoluteUri, relativePath);

        }
    }
}
