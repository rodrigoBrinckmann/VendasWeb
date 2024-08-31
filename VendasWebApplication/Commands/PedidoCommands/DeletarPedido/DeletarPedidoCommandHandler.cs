using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.PedidoCommands.DeletarPedido
{
    public class DeletarPedidoCommandHandler : IRequestHandler<DeletarPedidoCommand, Unit>
    {
        private readonly IPedidoRepository _pedidoRepository;
        public DeletarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Unit> Handle(DeletarPedidoCommand request, CancellationToken cancellationToken)
        {
            await _pedidoRepository.DeletarPedido(request.Id);
            return Unit.Value;
        }
    }
}
