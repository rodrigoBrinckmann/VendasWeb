using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebInfrastructure.Services.PasswordChangeNotificationService
{
    public interface IPasswordChangeNotificationService
    {
        void SendPasswordRetrieveEmailNotification(string email,string name, string newPassword);
        void SendPasswordChangeEmailNotification(string email, string name);
    }
}
