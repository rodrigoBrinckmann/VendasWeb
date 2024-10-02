using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;

namespace VendasWebCore.Services
{
    public interface IEmailService
    {
        Task<GmailService> ServiceMailProcess();
        Task SendEmailAsync(string email, string body, string title);
        Task<UserCredential> GetCredentialAsync();
    }
}
