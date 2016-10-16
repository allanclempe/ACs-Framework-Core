using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ACs.Angular.AspnetCore
{
    public class AngularServerMiddleware
    {
        private readonly AngularServerOptions _options;
        private readonly RequestDelegate _next;
        private readonly StaticFileMiddleware _innerMiddleware;

        public AngularServerMiddleware(RequestDelegate next, IHostingEnvironment env, ILoggerFactory loggerFactory, AngularServerOptions options)
        {
            _next = next;
            _options = options;


			_innerMiddleware = new StaticFileMiddleware(next, env, options.FileServerOptions, loggerFactory);
        }

        public async Task Invoke(HttpContext context)
        {
            // try to resolve the request with default static file middlewarex
            await _innerMiddleware.Invoke(context);
            Console.WriteLine(context.Request.Path + ": " + context.Response.StatusCode);
            // route to root path if the status code is 404
            // and need support angular html5mode
            if (context.Response.StatusCode == 404 && _options.Html5Mode)
            {
                context.Request.Path = _options.EntryPath;
                await _innerMiddleware.Invoke(context);
                Console.WriteLine(">> " + context.Request.Path + ": " + context.Response.StatusCode);
            }
        }
    }
}
