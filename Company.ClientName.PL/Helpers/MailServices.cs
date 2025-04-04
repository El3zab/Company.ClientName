using Company.ClientName.PL.Helpers.InterfacesHelpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Company.ClientName.PL.Helpers
{
    public class MailServices(IOptions<MailSettings> _options) : IMailService
    {
        //private readonly MailSettings _options;

        //public MailServices(IOptions<MailSettings> options) // "IOptions" Due Read Values From appsettings
        //{
        //    _options = options.Value;
        //}
        public bool SendEmail(Email email)
        {
            try
            {
                // Build Message
                var mail = new MimeMessage();

                mail.Subject = email.Subject;
                mail.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));
                mail.To.Add(MailboxAddress.Parse(email.To));

                var builder = new BodyBuilder();
                builder.TextBody = email.Body;
                mail.Body = builder.ToMessageBody();

                // Establish connection

                using var smpt = new SmtpClient();
                smpt.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smpt.Authenticate(_options.Value.Email, _options.Value.Password);

                // Send Message
                smpt.Send(mail);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
