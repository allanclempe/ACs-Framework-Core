using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ACs.Angular.AspnetCore
{
    public static class AngularServerExtension
    {
        public static IApplicationBuilder UseAngularServer(this IApplicationBuilder builder, string entryPath)
        {

            var logger  = (ILoggerFactory)builder.ApplicationServices.GetService(typeof(ILoggerFactory));
            var env = (IHostingEnvironment)builder.ApplicationServices.GetService(typeof(IHostingEnvironment));
            
            var fileProvider = new PhysicalFileProvider(env.WebRootPath);

	        var options = new OptionsManager<StaticFileOptions>(new[]
	        {
		        new ConfigureOptions<StaticFileOptions>(y =>
		        {
			        y.FileProvider = fileProvider;
			        y.RequestPath = new PathString(entryPath);
		        })
	        });


            builder.UseDefaultFiles(entryPath);

            return builder.Use(next => new AngularServerMiddleware(next, env, logger, new AngularServerOptions(options, entryPath)).Invoke);
        }
    }
}
