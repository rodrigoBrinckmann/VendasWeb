using MediatR;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Queries.GetItemPedidoById
{
    public class GetItemPedidoByIdQuery : IRequest<ItensPedidoViewModel>
    {
        public int Id { get; set; }
    }
}
