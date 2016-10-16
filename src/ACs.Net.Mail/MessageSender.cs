using System;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Linq;
using ACs.Net.Mail.Message;

namespace ACs.Net.Mail
{
    public class MessageSender : IMessageSender
    {
        private readonly ISmtpConfiguration _smtpConfiguration;
        private readonly ILogger _logger;

        public MessageSender(ISmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public MessageSender(ISmtpConfiguration smtpConfiguration, ILogger<MessageSender> logger)
            :this(smtpConfiguration)
        {
            _logger = logger;
        }

        public virtual Task<bool> SendEmailAsync(IHtmlMessage body)
        {
            return SendEmailAsync(body.MailTo, body.Subject, body.ToHtml());
        }

        public Task<bool> SendEmailAsync(string email, IHtmlMessage body)
        {
            return SendEmailAsync(email, body.Subject, body.ToHtml());
        }

        public virtual Task<bool> SendEmailAsync(string email, string subject, IHtmlMessage body)
        {
            return SendEmailAsync(email, subject, body.ToHtml());
        }

        public virtual Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            if (!_smtpConfiguration.Activated)
                return Task.FromResult(false);

            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new ArgumentException(nameof(email));

                if (string.IsNullOrEmpty(subject))
                    throw new ArgumentException(nameof(subject));

                if (string.IsNullOrEmpty(message))
                    throw new ArgumentException(nameof(message));

                var smtp = new SmtpClient(_smtpConfiguration.Server, _smtpConfiguration.Port)
                {
                    EnableSsl = _smtpConfiguration.UseSSL,
                    Timeout = _smtpConfiguration.Timeout
                };

                if (!string.IsNullOrEmpty(_smtpConfiguration.UserName))
                    smtp.Credentials = new NetworkCredential(_smtpConfiguration.UserName, _smtpConfiguration.Password);

                var mail = new MailMessage
                {
                    From = new MailAddress(_smtpConfiguration.From),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = message
                };

                var emails = email + ";";
                foreach(var to in emails.Split(';').Where(x=>!string.IsNullOrEmpty(x)))
                    mail.To.Add(new MailAddress(to));
                
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                _logger?.LogError("Message cannot be delivered.", e);

                return Task.FromResult(false);
            }
            
            return Task.FromResult(true);
        }

    

    }
}
