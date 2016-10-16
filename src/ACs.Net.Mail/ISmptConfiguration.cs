namespace ACs.Net.Mail
{
    public interface ISmtpConfiguration
    {
        string From { get; }
        string Server { get; }
        int Port { get; }
        string UserName { get; }
        string Password { get; }
        bool UseSSL { get; }
        bool Activated { get; }
        int Timeout { get; }
    }

   
}
