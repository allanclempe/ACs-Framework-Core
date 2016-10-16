using System;
using System.Collections.Generic;

namespace ACs.Net.Mail.Message
{
    public interface IHtmlMessage
    {
        string MailTo { get; }
        string Subject { get; }
        IHtmlMessage SetParam(string name, string value);
        IHtmlMessage SetParam(string name, Uri value);
        IHtmlMessage SetParams(IDictionary<string, object> values);
        IHtmlMessage SetParams(object values);
        IHtmlMessage SetFont(string fontName, int fontSize);
        string ToHtml();
    }
}
