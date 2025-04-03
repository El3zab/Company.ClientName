using System.Net;
using System.Net.Mail;

namespace Company.ClientName.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            // Mail Server : Gmail
            // SMTP => Simple Mail Transfer Protocol

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("ahmed.3zab99@gmail.com", "kblkvblcctnfkfbl"); // Sender
                client.Send("ahmed.3zab99@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
