using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ACs.Net.Mail;
using ACs.Net.Mail.Message;
using HtmlAgilityPack;

namespace ACs.Net.Mail.Tests
{

    public class HtmlParameterReplacementTest
    {

        [Fact]
        public void ReplaceWithDynamic()
        {
            var template = new HtmlDocument();
            template.LoadHtml("<div><param name=\"url\" /> and <param name=\"another\" />");

            var uri = new Uri("Http://somedomain.com/test");
            var result = HtmlParameterReplacement.Replace(template, new { url = new Uri("http://somedomain.com/test"), another = "param" });
            var html = result.DocumentNode.OuterHtml;

            Assert.Null(result.DocumentNode.SelectNodes("//param"));
            Assert.Contains("<a href", result.DocumentNode.OuterHtml);
            Assert.Contains("target=\"_blank\"", result.DocumentNode.OuterHtml);
            Assert.Contains(uri.ToString(), result.DocumentNode.OuterHtml);
            Assert.Contains("<span>param</span>", result.DocumentNode.OuterHtml);
        }

        [Fact]
        public void ReplaceUrl()
        {
            var template = new HtmlDocument();
            template.LoadHtml("<div><param name=\"url\"/></div>");

            var uri = new Uri("Http://somedomain.com/test");
            var result = HtmlParameterReplacement.Replace(template, "url", uri);

            Assert.Contains("<a href", result.DocumentNode.OuterHtml);
            Assert.Contains(uri.ToString(), result.DocumentNode.OuterHtml);
            Assert.Contains("target=\"_blank\"", result.DocumentNode.OuterHtml);
        }

        [Fact]
        public void ReplaceString()
        {
            var template = new HtmlDocument();
            template.LoadHtml("<div>Your name is <param name=\"name\"/>.</div>");

            var userName = "User Test";
            var result = HtmlParameterReplacement.Replace(template, "name", userName);

            Assert.Equal($"<div>Your name is <span>{userName}</span>.</div>", result.DocumentNode.OuterHtml);
        }

        [Fact]
        public void ReplaceByDictionary()
        {
            var template = new HtmlDocument();
            template.LoadHtml("<div>Your name is <param name=\"name\"/>.</div>");

            var userName = "User test";

            var result = HtmlParameterReplacement.Replace(template, new Dictionary<string,object>()  {
                { "name", userName }
            });
            
            Assert.Equal($"<div>Your name is <span>{userName}</span>.</div>", result.DocumentNode.OuterHtml);
        }



        [Fact]
        public void NullParameter()
        {
            var template = new HtmlDocument();
            template.LoadHtml("<div>Your name is <param name=\"name\"/>.</div>");

            
            var result = HtmlParameterReplacement.Replace(template, new Dictionary<string, object>()  {
                { "name", null }
            });

            Assert.Equal("<div>Your name is <span></span>.</div>", result.DocumentNode.OuterHtml);
        }


    }
}
