using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectECommerce.Utility
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings _emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
                
        }
        public async Task Execute(string email,string subject,string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ?
                    _emailSettings.ToEmail : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "My Email Name")
                };
                mail.To.Add(toEmail);
                mail.CC.Add(_emailSettings.CcEmail);
                mail.Subject = "Shopping App : " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                using(SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain,
                    _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential
                        (_emailSettings.UsernameEmail, _emailSettings.UserNamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Execute(email, subject, htmlMessage).Wait();
            return Task.FromResult(0);
        }
        
    }
}
