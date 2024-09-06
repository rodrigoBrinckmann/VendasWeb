using Google.Apis.Gmail.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.Services
{
    public interface IEmailService
    {
        Task ServiceMailProcess(string toEmail, string password);
        Task SendEmailAsync(GmailService service, string password, string email);
    }
}
