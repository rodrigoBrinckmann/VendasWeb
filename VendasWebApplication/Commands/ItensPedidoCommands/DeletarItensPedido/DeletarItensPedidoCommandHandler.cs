using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido
{
    public class DeletarItensPedidoCommandHandler : IRequestHandler<DeletarItensPedidoCommand,Unit>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public DeletarItensPedidoCommandHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task<Unit> Handle(DeletarItensPedidoCommand request, CancellationToken cancellationToken)
        {
            await _itensPedidoRepository.DeletarItensPedido(request.Id);
            return Unit.Value;
        }
    }
}
