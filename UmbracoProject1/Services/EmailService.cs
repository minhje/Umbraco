using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

// Hjälp tagen av ChatGTP 5. 
namespace UmbracoProject1.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        private readonly string _senderAddress;

        public EmailService(IConfiguration config)
        {
            var connectionString = config["AzureCommunication:ConnectionString"];
            _emailClient = new EmailClient(connectionString);
            _senderAddress = "DoNotReply@55556ded-58a7-44ae-9b19-3cbabb1a1faf.azurecomm.net";
        }

        public async Task SendEmailAsync(string to)
        {
            var emailContent = new EmailContent("Thank you")
            {
                PlainText = "Thank you for your email, we will get in touch with you shortly.",
                Html = @"
                <html>
                  <head>
                     <style>
                        body {
                            font-family: 'Poppins', Arial, sans-serif;
                            background-color: #FFFFFF;
                            margin: 0;
                            padding: 0;
                        }
                        .container {
                            max-width: 600px;
                            margin: 40px auto;
                            background-color: #F7F7F7;
                            border-radius: 10px;
                            overflow: hidden;
                        }
                        .header {
                            color: #4F5955;;
                            text-align: center;
                            padding: 30px 20px 20px;
                        }
                        .header img {
                            display: block;
                            margin: 0 auto 20px;
                            max-width: 150px;
                            height: auto;
                        }
                        .header h1 {
                            margin: 0;
                            font-size: 24px;
                        }
                        .content {
                            padding: 30px;
                            color: #333;
                        }
                        .content p {
                            font-size: 16px;
                            line-height: 1.5;
                        }
                        .footer {
                            text-align: center;
                            padding: 15px;
                            font-size: 13px;
                            color: #4F5955;
                        }
                        a.button {
                            display: inline-block;
                            padding: 10px 20px;
                            background-color: #4F5955;
                            color: white;
                            text-decoration: none;
                            border-radius: 5px;
                            margin-top: 20px;
                        }
                        a.button:hover {
                            background-color: #D9C3A9;
                        }
                    </style>
                  </head>
                  <body>
                    <div class='container'>
                        <div class='header'>
                            <img src='https://onatrix-5959470f-bfeghrhqagh9evbw.swedencentral-01.azurewebsites.net/media/c24d5ihw/brand-logo.svg'></img>
                            <h1>Thank you for your email - we'll get in touch with you shortly!</h1>
                        </div>
                        <div class='content'>
                            <p> We have received your email and will contact you as soon as possible. </p>
                            <p> Until then, feel free to visit our website and learn more about our services: </p>
                            <a href='https://onatrix-5959470f-bfeghrhqagh9evbw.swedencentral-01.azurewebsites.net/' class='button'>Visit us</a>
                        </div>
                        <div class='footer'> 
                          <p>This is a automatic email - please do not reply.</p>
                        </div>
                    </div>
                  </body>
                </html>"
            };

            //var recipients = new EmailRecipients(new List<EmailAddress>
            //{
            //    new EmailAddress(to)
            //});

            //var emailMessage = new Azure.Communication.Email.EmailMessage(
            //    _senderAddress,
            //    recipients,
            //    emailContent
            //);

            var emailMessage = new EmailMessage(
            _senderAddress,
            new EmailRecipients(new[] { new EmailAddress(to) }),
            emailContent
            );

            EmailSendOperation emailSendOperation = await _emailClient.SendAsync(
                WaitUntil.Completed,
                emailMessage
            );
        }
    }
}
