using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetAllPedidos
{
    public class GetAllPedidosQueryHandler : IRequestHandler<GetAllPedidosQuery, PaginationResult<PedidoViewModel>>
    {
        private readonly IPedidoRepository _pedidoRepository;
        
        public GetAllPedidosQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PaginationResult<PedidoViewModel>> Handle(GetAllPedidosQuery request, CancellationToken cancellationToken)
        {
            return await _pedidoRepository.ListarPedidos(request.Query, request.Page);           
        }
    }
}
