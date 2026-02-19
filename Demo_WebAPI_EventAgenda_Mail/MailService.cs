using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Demo_WebAPI_EventAgenda_Mail
{
    public class MailKitConfig : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _appName;
        private readonly string _emailApp;
        private readonly string _emailPassword;
        private readonly string _emailUser;

        public MailKitConfig(IConfiguration configuration)
        {
            var section = configuration.GetSection("Smtp");

            _smtpHost = section["smtpHost"];
            _smtpPort = int.Parse(section["smtpPort"] ?? "0");
            _appName = section["appName"];
            _emailApp = section["emailApp"];
            _emailPassword = section["emailPassword"];
            _emailUser = section["emailUser"];
        }

        public void SendWelcomeEmail(Member member)
        {
            MimeMessage message = new MimeMessage();

            // Base information
            message.From.Add(new MailboxAddress(_appName, _emailApp));
            // Fixed: use MailboxAddress.Parse because MailboxAddress has no single-argument constructor
            message.To.Add(MailboxAddress.Parse(member.Email));
            message.Subject = "Test email from Demo App";

            // Create the email body
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "This is a test email from Demo App."; // This is for the old email clients

            // Add an HTML version of the email body
            bodyBuilder.HtmlBody = $@"
            <div>
	             <h1>Hello {member.Email},</h1>
                 <p>Thank you very much for creating an account in our App!🤓🏆</p>
            </div>";

            message.Body = bodyBuilder.ToMessageBody();

            // Send the email
            using (SmtpClient smtpClient = new SmtpClient())
            {
                try
                {
                    // Connect to the SMTP server
                    smtpClient.Connect(_smtpHost, _smtpPort, false);
                    // Authenticate if necessary (not needed for localhost without authentication)
                    smtpClient.Authenticate(_emailUser, _emailPassword);
                    // Send the email
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                finally
                {

                    // Disconnect from the SMTP server
                    smtpClient.Disconnect(true);
                }

            }
        }
    }
}
