using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.PedidoCommands.EditarPedido
{
    public class EditarPedidoCommandHandler : IRequestHandler<EditarPedidoCommand, Pedido>
    {
        private readonly IPedidoRepository _pedidoRepository;
        public EditarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<Pedido> Handle(EditarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido(request.NomeCliente, request.EmailCliente);
            return await _pedidoRepository.EditarPedidoAsync(request.Id, pedido);
        }
    }
}
