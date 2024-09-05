using MediatR;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;

namespace VendasWebApplication.Queries.GetAllPedidos
{
    public class GetAllPedidosQuery : IRequest<PaginationResult<PedidoViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
