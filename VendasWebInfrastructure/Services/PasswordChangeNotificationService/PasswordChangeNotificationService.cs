using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.DefaultMessages;
using VendasWebCore.Entities;
using VendasWebCore.Services;

namespace VendasWebInfrastructure.Services.PasswordChangeNotificationService
{
    public class PasswordChangeNotificationService : IPasswordChangeNotificationService
    {
        private readonly IEmailService _emailService;

        public PasswordChangeNotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }


        public async void SendPasswordRetrieveEmailNotification(string email, string name, string newPassword)
        {
            var content = string.Format(TemplateMessages.EmailPasswordRetrieveChange,name,newPassword);
            await _emailService.SendEmailAsync(email, content, TemplateMessages.PasswordChangeTitle);
        }

        public async void SendPasswordChangeEmailNotification(string email, string name)
        {
            var content = string.Format(TemplateMessages.EmailPasswordChange, name);
            await _emailService.SendEmailAsync(email, content, TemplateMessages.PasswordChangeTitle);
        }
    }
}
