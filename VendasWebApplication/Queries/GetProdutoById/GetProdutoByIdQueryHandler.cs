using MediatR;
using VendasWebApplication.ViewModels;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Queries.GetProdutoById
{
    public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, ProdutoViewModel>
    {
        private readonly IProdutoRepository _produtoRepository;
        public GetProdutoByIdQueryHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoViewModel> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ListarProdutoEspecífico(request.Id);
                        
            var produtoResult = new ProdutoViewModel(
                produto.IdProduto,
                produto.NomeProduto,
                produto.Valor
                );
            return produtoResult;            
        }        
    }
}

