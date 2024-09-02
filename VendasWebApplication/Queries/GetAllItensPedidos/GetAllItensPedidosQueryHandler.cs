using MediatR;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;
using VendasWebInfrastructure.Persistence.Repositories;

namespace VendasWebApplication.Queries.GetAllItensPedidos
{
    public class GetAllItensPedidosQueryHandler : IRequestHandler<GetAllItensPedidosQuery, PaginationResult<ItensPedido>>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public GetAllItensPedidosQueryHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }
        public async Task<PaginationResult<ItensPedido>> Handle(GetAllItensPedidosQuery request, CancellationToken cancellationToken)
        {
            var paginatedResults = await _itensPedidoRepository.ListarItensPedido(request.Query, request.Page);
            
            var paginationResult = new PaginationResult<ItensPedido>(
               paginatedResults.Page,
               paginatedResults.TotalPages,
               paginatedResults.PageSize,
               paginatedResults.ItemsCount,
               paginatedResults.Data
               );
            return paginationResult;
        }
    }
}
