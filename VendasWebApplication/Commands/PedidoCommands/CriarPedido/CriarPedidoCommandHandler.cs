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
    public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, Unit>
    {
        private readonly IPedidoRepository _pedidoRepository;
        public CriarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<Unit> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido(request.NomeCliente, request.EmailCliente, request.DataCriacao, request.Pago);
            await _pedidoRepository.CadastrarPedidoAsync(pedido);
            return Unit.Value;
        }
    }
}
