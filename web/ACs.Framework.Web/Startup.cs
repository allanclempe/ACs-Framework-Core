using System;
using System.IO;
using ACs.Framework.Web.Core.Events;
using ACs.Framework.Web.Core.Infra;
using ACs.Framework.Web.Data;
using ACs.Net.Mail;
using ACs.NHibernate;
using ACs.Security.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ACs.NHibernate.AspnetCore;
using ACs.Angular.AspnetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace ACs.Framework.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables()
                .AddUserSecrets();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services
		        .AddOptions();

			services
				.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton(x => DatabaseFactory.BuildSessionFactory(Configuration.GetSection("NHibernate").ToDictionary()))
				.Configure<JwtTokenConfiguration>(Configuration.GetSection("Security"))
				.Configure<SmtpConfiguration>(Configuration.GetSection("Smtp"))
				.AddSingleton<ISmtpConfiguration, SmtpConfiguration>(x=>x.GetService<IOptions<SmtpConfiguration>>().Value)
				.AddSingleton<IJwtTokenConfiguration, JwtTokenConfiguration>(x => x.GetService<IOptions<JwtTokenConfiguration>>().Value)
				.AddSingleton<IJwtTokenProvider, JwtTokenProvider>()
                .AddSingleton<ISystemConfiguration, SystemConfiguration>()
                .AddSingleton<IMessageSender, MessageSender>()
                .AddScoped<IDatabaseFactory, DatabaseFactory>()
                //events
                .AddScoped<IUserEvent, UserEvent>()
                .AddScoped<IUserEvent, UserEvent>()
                //repo
                .AddScoped<IUserRepository, UserRepository>()
                ;

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			var provider = app.ApplicationServices.GetService<IJwtTokenProvider>();

	        var options = new JwtBearerOptions
	        {
		        AutomaticAuthenticate = true,
				TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = provider.Key,
					ValidAudience = provider.Audience,
					ValidIssuer = provider.Issuer
				}
	        };

	        app
		        .UseMiddleware<JwtMiddleware500to401Error>()
		        .UseJwtBearerAuthentication(options)
		        .UseStaticContext()
				.UseStaticFiles(new StaticFileOptions
				{
					ServeUnknownFileTypes = true,
					DefaultContentType = "image/png",
				})
				.UseMvc()
				.UseFileServer()
				.UseAngularServer("/index.html");


        }
    }
}
