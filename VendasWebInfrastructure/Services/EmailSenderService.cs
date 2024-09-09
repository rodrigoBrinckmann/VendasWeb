using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MimeKit;
using VendasWebCore.Services;
using Google.Apis.Gmail.v1;


using Google.Apis.Gmail.v1.Data;
using Microsoft.Extensions.Configuration;


namespace VendasWebInfrastructure.Services
{
    public class EmailSenderService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailSenderService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmailAsync(GmailService service, string password, string email)
        {
            var emailMessage = new MimeMessage
            {
                From = { new MailboxAddress("Rodrigo", "rodrigo.brinckmann@gmail.com") },
                To = { new MailboxAddress("Vendas WebApp", email) },
                Subject = "Password Change",
                Body = new TextPart("plain") { Text = $"Your new password is {password}" }
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

                await service.Users.Messages.Send(request, "me").ExecuteAsync();
            }

                Console.WriteLine("Email sent successfully!");
        }

        public async Task ServiceMailProcess(string toEmail, string password)
        {
            // Obtém o token de acesso
            var credential = await GetCredentialAsync();

            // Cria o serviço Gmail
            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "VendasWebApp",
            });

            // Envia o e-mail
            await SendEmailAsync(service, password, toEmail);
        }

        public async Task<UserCredential> GetCredentialAsync()
        {
            // Configura o Client ID e o Client Secret
            var clientId = _config["GoogleApi:ClientId"];
            var clientSecret = _config["GoogleApi:ClientSecret"];

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                new[] { GmailService.Scope.GmailSend },
                "rodrigo.brinckmann@gmail.com",
                CancellationToken.None,
                new FileDataStore("GmailOAuth2Token")
            );

            return credential;
        }
    }
}
