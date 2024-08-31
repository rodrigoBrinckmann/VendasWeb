using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetPedidoById
{

    public class GetPedidoByIdQueryHandler : IRequestHandler<GetPedidoByIdQuery, PedidoViewModel>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidoByIdQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoViewModel> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _pedidoRepository.ListarPedidoEspecífico(request.Id);


        }
    }
}
