using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;

namespace VendasWebCore.Services
{
    public interface IEmailService
    {
        Task ServiceMailProcess(string toEmail, string password);
        Task SendEmailAsync(GmailService service, string password, string email);
        Task<UserCredential> GetCredentialAsync();
    }
}
