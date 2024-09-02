using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Queries.GetItemPedidoById
{
    public class GetItemPedidoByIdQueryHandler : IRequestHandler<GetItemPedidoByIdQuery, ItensPedido>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public GetItemPedidoByIdQueryHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task<ItensPedido> Handle(GetItemPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _itensPedidoRepository.ListarItensPedidoEspecífico(request.Id);
        }
    }
}
