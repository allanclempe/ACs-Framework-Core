using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACs.Net.Mail.Message;
using Xunit;

namespace ACs.Net.Mail.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class HtmlMessageTest
    {
        private const string _templateHtml = "<div>Welcome <param name=\"name\" /><br><br>To activate your account click link above.<br><br><param name=\"link\" /><br><br>Thanks</div>";
        private const string _templateHtmlWithMetas = "<meta name=\"mailto\" value=\"email@domain.com\"><meta name=\"subject\" value=\"Activate your account\"><div>Welcome <param name=\"name\" /><br><br>To activate your account click link above.<br><br><param name=\"link\" /><br><br>Thanks</div>";
        private const string _templateHtmlWithBody = "<html><head><meta name=\"mailto\" value=\"email@domain.com\"><meta name=\"subject\" value=\"Activate your account\"></head><body><div>Welcome <param name=\"name\" /><br><br>To activate your account click link above.<br><br><param name=\"link\" /><br><br>Thanks</div></body></html>";

        [Fact]
        public void CreateBody()
        {
            var name = "User Name";
            var uri = new Uri("Http://somedomain.com");

            var body = new HtmlMessage(_templateHtml)
                .SetParams(new
                {
                    name = name,
                    link = uri
                });


            var result = body.ToHtml();
            Assert.StartsWith("<!DOCTYPE", result);
            Assert.Contains("<br>", result, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains(name, result);
            Assert.Contains("a href", result);
            Assert.Contains(uri.ToString(), result);

        }

        [Fact]
        public void SetFontFamilyAndSize()
        {
            var result = new HtmlMessage(_templateHtml)
                .SetFont("arial", 12)
                .ToHtml();

            Assert.Contains("font-family:'arial'", result, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains("font-size:12", result, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void SetMetaTags()
        {
            var body = new HtmlMessage(_templateHtmlWithMetas);

            Assert.Equal("email@domain.com", body.MailTo);
            Assert.Equal("Activate your account", body.Subject);

        }

        [Fact]
        public void VerifyIfBodyIsReplacingByDefaultTempalte()
        {
            var body = new HtmlMessage(_templateHtmlWithBody);

            Assert.StartsWith("<!DOCTYPE", body.ToHtml());
            Assert.Null(body.Html.DocumentNode.SelectNodes("//meta"));
            Assert.Equal("email@domain.com", body.MailTo);
            Assert.Equal("Activate your account", body.Subject);

        }

    }
}
