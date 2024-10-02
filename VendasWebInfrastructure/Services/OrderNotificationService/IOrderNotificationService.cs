using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebInfrastructure.Services.OrderNotificationService
{
    public interface IOrderNotificationService
    {
        void PublishOrderNotification(object orderData);
        void SendOrderEmailNotification(string name, string email, int pedidoId);
    }
}
