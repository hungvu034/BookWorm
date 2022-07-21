using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BookWorn.Service
{
    public class MailSender : IEmailSender
    {
       
        private MailSettings settings ; 
        public MailSender(IOptions<MailSettings> options){
            this.settings = options.Value ; 
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MimeKit.MimeMessage message = new MimeKit.MimeMessage();
            message.Subject = subject ;
            message.From.Add(new MailboxAddress(settings.DisplayName , settings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlMessage ;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(settings.Host , settings.Port); 
    
            await client.AuthenticateAsync(settings.Mail , settings.Password);
            
            await client.SendAsync(message);
            client.Disconnect(true);
            
        }

        public class MailSettings{
            public string Mail { get; set; }
            public string DisplayName { get; set; }
            public string Password { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }
    }
}