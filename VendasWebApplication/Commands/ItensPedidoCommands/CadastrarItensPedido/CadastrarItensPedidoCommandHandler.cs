using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.ItensPedidoCommands.CadastrarItensPedido
{
    public class CadastrarItensPedidoCommandHandler : IRequestHandler<CadastrarItensPedidoCommand, Unit>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public CadastrarItensPedidoCommandHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task<Unit> Handle(CadastrarItensPedidoCommand request, CancellationToken cancellationToken)
        {
            await _itensPedidoRepository.CadastrarItensPedidoAsync(request.ItensPedido);
            return Unit.Value;
        }

        public Task<Unit> Handle(IList<ItensPedido> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
