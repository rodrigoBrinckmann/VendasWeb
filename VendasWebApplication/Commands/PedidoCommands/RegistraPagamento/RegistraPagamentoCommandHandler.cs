using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.PedidoCommands.RegistraPagamento
{
    public class RegistraPagamentoCommandHandler : IRequestHandler<RegistraPagamentoCommand, Pedido>
    {
        private readonly IPedidoRepository _pedidoRepository;
        public RegistraPagamentoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> Handle(RegistraPagamentoCommand request, CancellationToken cancellationToken)
        {
            return await _pedidoRepository.RegistrarPagamentoPedidoAsync(request.Id, request.Pago);
        }
    }
}
