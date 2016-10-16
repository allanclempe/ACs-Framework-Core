using ACs.Net.Mail.Message;

namespace ACs.Framework.Web.Core.Infra
{
    public interface ISystemConfiguration
    {
        IHtmlMessage GetMailMessage(EmailTemplate emailTemplate);
    }
}
