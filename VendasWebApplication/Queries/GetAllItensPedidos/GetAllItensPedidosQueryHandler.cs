using MediatR;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;
using VendasWebInfrastructure.Persistence.Repositories;

namespace VendasWebApplication.Queries.GetAllItensPedidos
{
    public class GetAllItensPedidosQueryHandler : IRequestHandler<GetAllItensPedidosQuery, PaginationResult<ItensPedidoViewModel>>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public GetAllItensPedidosQueryHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }
        public async Task<PaginationResult<ItensPedidoViewModel>> Handle(GetAllItensPedidosQuery request, CancellationToken cancellationToken)
        {
            var paginatedResults = await _itensPedidoRepository.ListarItensPedido(request.Query, request.Page);

            var itensPedidosViewModel = new List<ItensPedidoViewModel>();
            foreach (var result in paginatedResults.Data)
            {
                itensPedidosViewModel.Add(new ItensPedidoViewModel()
                {
                    IdPedido = result.IdPedido,
                    IdProduto = result.IdProduto,
                    Pedido = result.Pedido,
                    Produto = result.Produto,
                    Quantidade = result.Quantidade
                });
            }

            var paginationResult = new PaginationResult<ItensPedidoViewModel>(
               paginatedResults.Page,
               paginatedResults.TotalPages,
               paginatedResults.PageSize,
               paginatedResults.ItemsCount,
               itensPedidosViewModel
               );
            return paginationResult;
        }
    }
}
