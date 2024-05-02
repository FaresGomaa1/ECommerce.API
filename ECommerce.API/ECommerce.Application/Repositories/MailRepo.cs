using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Application.Repositories;
using ECommerce.API.ECommerce.Domain;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class MailRepo : IMailRepo
    {
        private readonly MailSetting mailSetting;

        public MailRepo(IOptions<MailSetting> mailOptions)
        {
            mailSetting = mailOptions.Value;
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(mailSetting.Email),
                    Subject = subject,
                };
                email.To.Add(MailboxAddress.Parse(mailTo));
                var builder = new BodyBuilder();
                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                email.From.Add(new MailboxAddress(mailSetting.DisplayName, mailSetting.Email));
                using var smtp = new SmtpClient();
                smtp.Connect(mailSetting.Host, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSetting.Email, mailSetting.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}