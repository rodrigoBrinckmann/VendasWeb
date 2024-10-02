using Newtonsoft.Json.Linq;
using VendasWebCore.DefaultMessages;
using VendasWebCore.Services;
using VendasWebInfrastructure.MessageBus;

namespace VendasWebInfrastructure.Services.OrderNotificationService
{
    public class OrderNotificationService : IOrderNotificationService
    {
        private readonly IMessageBusClient _messageBus;
        private readonly IEmailService _emailService;

        public OrderNotificationService(IEmailService emailService, IMessageBusClient messageBus)
        {
            _emailService = emailService;
            _messageBus = messageBus;
        }
        public void PublishOrderNotification(object orderData)
        {
            var routingKey = "Order-created";
            _messageBus.Publish(orderData, routingKey, "pedidos");
        }

        public async void SendOrderEmailNotification(string name, string email, int pedidoId)
        {
            var content = string.Format(TemplateMessages.OrderCreated, name, pedidoId);            
            await _emailService.SendEmailAsync(email, content, TemplateMessages.OrderCreatedTitle);
        }
    }
}
