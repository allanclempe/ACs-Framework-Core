using System.IO;
using ACs.Framework.Web.Core.Infra;
using ACs.Net.Mail.Message;
using Microsoft.AspNetCore.Hosting;

namespace ACs.Framework.Web
{
    public class SystemConfiguration : ISystemConfiguration
    {
        private readonly IHostingEnvironment _environment;

        public SystemConfiguration(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IHtmlMessage GetMailMessage(EmailTemplate emailTemplate)
        {
            using (var sr = File.OpenText(Path.Combine(_environment.WebRootPath, $"/emails/{emailTemplate}.html")))
              return new HtmlMessage(sr);
        }

      
    }
}
