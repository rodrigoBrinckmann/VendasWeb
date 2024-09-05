using MediatR;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Queries.GetProdutoById
{
    public class GetProdutoByIdQuery : IRequest<ProdutoViewModel>
    {        
        public int Id { get; set; }
    }
}
