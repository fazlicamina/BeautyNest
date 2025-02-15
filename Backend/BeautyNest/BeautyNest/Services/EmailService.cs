using System.Net.Mail;
using System.Net;

namespace BeautyNest.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            if (!int.TryParse(_config["Email:Port"], out int port))
            {
                throw new Exception("Nevažeća SMTP konfiguracija: Port nije ispravan.");
            }

            using var client = new SmtpClient(_config["Email:SmtpServer"], port)
            {
                Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Email:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);
            await client.SendMailAsync(mailMessage);
        }


    }
}
