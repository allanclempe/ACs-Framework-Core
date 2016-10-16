using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Reflection;

namespace ACs.Net.Mail.Message
{
    public static class HtmlParameterReplacement
    {
        public static HtmlDocument Replace(HtmlDocument document, object parameters)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var propertyInfo in parameters.GetType().GetTypeInfo().DeclaredProperties.Where(propertyInfo => propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0))
                dictionary[propertyInfo.Name] = propertyInfo.GetValue(parameters, null);

            return Replace(document, dictionary);
        }

        public static HtmlDocument Replace(HtmlDocument document, IDictionary<string, object> parameters)
        {
            return parameters.Aggregate(document, (current, replace) => Process(current, replace.Key, replace.Value));
        }

        public static HtmlDocument Replace(HtmlDocument document, string key, string value)
        {
            return Process(document, key, value);
        }

        public static HtmlDocument Replace(HtmlDocument document, string key, Uri value)
        {
            return Process(document, key, value);
        }

        private static HtmlDocument Process(HtmlDocument document, string key, object value)
        {

            if (value == null) value = "";

            var toTag = HtmlNode.CreateNode("<span></span>");

            if (value.GetType() == typeof(Uri))
            {
                var aTag = HtmlNode.CreateNode("<a></a>");
                aTag.Attributes.Add("href", ((Uri)value).ToString());
                aTag.Attributes.Add("target", "_blank");
                toTag.AppendChild(aTag);                
            }
            else
            {
                toTag.InnerHtml = value.ToString();
            }


            var nodes = document.DocumentNode.SelectNodes("//param[@name=\"" + key + "\"]");

            if (nodes == null || nodes.Count == 0)
                return document;

            foreach (var param in nodes)
                param.ParentNode.ReplaceChild(toTag, param);

            return document;
        }
    }
}
