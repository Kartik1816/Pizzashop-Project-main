using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PizzaShop.Domain.Models;
using PIzzaShop.Service.Interfaces;
using System;
using System.Threading.Tasks;



namespace PIzzaShop.Service.Services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, User user, string resetLink)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_configuration["SmtpSettings:SenderName"], _configuration["SmtpSettings:SenderEmail"]));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = "Reset your password";

            var emailTemplate = System.IO.File.ReadAllText("Views/EmailTemplet/ForgotPasswordEmailTemplet.html");
            emailTemplate = emailTemplate.Replace("{{resetLink}}", resetLink);

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailTemplate
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

         public async Task SendAddUserEmailAsync(string Email, string Password)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_configuration["SmtpSettings:SenderName"], _configuration["SmtpSettings:SenderEmail"]));
            emailMessage.To.Add(new MailboxAddress("", Email));
            emailMessage.Subject = "You Are Added to our application";

            var emailTemplate = System.IO.File.ReadAllText("Views/EmailTemplet/addUser.html");
            emailTemplate = emailTemplate.Replace("{{email}}", Email);
            emailTemplate = emailTemplate.Replace("{{password}}", Password);

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailTemplate
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
       
    }
}