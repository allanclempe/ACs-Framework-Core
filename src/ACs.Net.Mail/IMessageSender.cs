using System.Threading.Tasks;
using ACs.Net.Mail.Message;

namespace ACs.Net.Mail
{
    public interface IMessageSender
    {
        Task<bool> SendEmailAsync(IHtmlMessage body);
        Task<bool> SendEmailAsync(string email, IHtmlMessage body);
        Task<bool> SendEmailAsync(string email, string subject, IHtmlMessage body);
        Task<bool> SendEmailAsync(string email, string subject, string message);        
    }
}
