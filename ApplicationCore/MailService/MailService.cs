using ApplicationCore.Dto.MailServiceDto;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.MailService
{
    public class MailService
    {
        private readonly SmtpSettings _smtpSettings;
        public MailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task SendEmailAsync(string toEmail,string subject,string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject =subject;

            email.Body = new TextPart("html") { Text = body };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSSL ? MailKit.Security.SecureSocketOptions.SslOnConnect :
                _smtpSettings.UseStartTls ? MailKit.Security.SecureSocketOptions.StartTls : MailKit.Security.SecureSocketOptions.None);

            await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
