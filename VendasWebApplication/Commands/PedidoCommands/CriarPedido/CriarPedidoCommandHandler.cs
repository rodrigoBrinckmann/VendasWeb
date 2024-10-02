using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebInfrastructure.MessageBus;
using VendasWebInfrastructure.Persistence.Repositories;
using VendasWebInfrastructure.Services.OrderNotificationService;

namespace VendasWebApplication.Commands.PedidoCommands.CriarPedido
{
    public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, int>
    {
        private readonly IPedidoRepository _pedidoRepository;        
        private readonly IOrderNotificationService _orderNotificationService;

        public CriarPedidoCommandHandler(IPedidoRepository pedidoRepository, IOrderNotificationService orderNotificationService)
        {
            _pedidoRepository = pedidoRepository;
            _orderNotificationService = orderNotificationService;
        }
        public async Task<int> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido(request.UserId, request.DataCriacao, request.Pago, request.ItensPedidos);
            await _pedidoRepository.CadastrarPedidoAsync(pedido);

            var pedidoEfetuado = await _pedidoRepository.ListarPedidoEspecífico(pedido.IdPedido);
            _orderNotificationService.PublishOrderNotification(pedido);
            _orderNotificationService.SendOrderEmailNotification(pedidoEfetuado.Cliente.FullName, pedidoEfetuado.Cliente.Email, pedido.IdPedido); //edido.Cliente.Email

            return pedido.IdPedido;
        }
    }
}
