using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebInfrastructure.Persistence.Repositories;

namespace VendasWebApplication.Commands.PedidoCommands.CriarPedido
{
    public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, int>
    {
        private readonly IPedidoRepository _pedidoRepository;
        public CriarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<int> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido(request.UserId, request.DataCriacao, request.Pago, request.ItensPedidos);
            await _pedidoRepository.CadastrarPedidoAsync(pedido);
            return pedido.IdPedido;
        }
    }
}
