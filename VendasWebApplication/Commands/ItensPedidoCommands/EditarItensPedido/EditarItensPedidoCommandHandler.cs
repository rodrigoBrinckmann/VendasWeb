using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.PedidoCommands.EditarPedido;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido
{
    public class EditarItensPedidoCommandHandler : IRequestHandler<EditarItensPedidoCommand, ItensPedido>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public EditarItensPedidoCommandHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task<ItensPedido> Handle(EditarItensPedidoCommand request, CancellationToken cancellationToken)
        {
            ItensPedido itensPedido = new ItensPedido() {IdPedido = request.IdPedido, IdProduto = request.IdProduto, Quantidade = request.Quantidade };
            return await _itensPedidoRepository.EditarItensPedidoAsync(request.Id, itensPedido);
        }
    }
}
