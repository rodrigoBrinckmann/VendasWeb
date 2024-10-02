using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MimeKit;
using VendasWebCore.Services;
using Google.Apis.Gmail.v1;


using Google.Apis.Gmail.v1.Data;
using Microsoft.Extensions.Configuration;
using VendasWebCore.DefaultMessages;


namespace VendasWebInfrastructure.Services.EmailService
{
    public class EmailSenderService : IEmailService
    {        
        private readonly MailConfig _mailConfig;
        public EmailSenderService(MailConfig mailConfig)
        {     
            _mailConfig = mailConfig;
        }

        public async Task SendEmailAsync(string toEmail, string content, string title)
        {
            var googleService = await ServiceMailProcess();

            var emailMessage = new MimeMessage
            {
                From = { new MailboxAddress(_mailConfig.FromName, _mailConfig.FromEmail) },
                To = { new MailboxAddress("Nome do Cliente", toEmail) },
                Subject = title, //add other options in parameters
                Body = new TextPart("plain") { Text = content } //add more options in a constants folder
            };

            using (var stream = new MemoryStream())
            {
                await emailMessage.WriteToAsync(stream);
                var rawMessage = Convert.ToBase64String(stream.ToArray())
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", "");

                var request = new Message
                {
                    Raw = rawMessage
                };

                await googleService.Users.Messages.Send(request, "me").ExecuteAsync();
            }

            Console.WriteLine("Email sent successfully!");
        }

        public async Task<GmailService> ServiceMailProcess()
        {
            // Obtém o token de acesso
            var credential = await GetCredentialAsync();

            // Cria o serviço Gmail
            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "VendasWebApp",
            });

            return service;            
        }

        public async Task<UserCredential> GetCredentialAsync()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _mailConfig.GoogleClientId,
                    ClientSecret = _mailConfig.GoogleClientSecret
                },
                new[] { GmailService.Scope.GmailSend },
                _mailConfig.FromEmail,
                CancellationToken.None,
                new FileDataStore("GmailOAuth2Token")
            );

            return credential;
        }
    }
}
