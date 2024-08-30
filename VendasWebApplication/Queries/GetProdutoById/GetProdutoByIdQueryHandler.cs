using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

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

