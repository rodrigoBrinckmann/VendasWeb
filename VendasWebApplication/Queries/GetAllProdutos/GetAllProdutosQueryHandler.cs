using Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VendasWebApplication.Queries.GetAllProdutos
{
    public class GetAllProdutosQueryHandler : IRequestHandler<GetAllProdutosQuery, PaginationResult<ProdutoViewModel>>
    {
        private readonly IProdutoRepository _produtoRepository;
        public GetAllProdutosQueryHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<PaginationResult<ProdutoViewModel>> Handle(GetAllProdutosQuery request, CancellationToken cancellationToken)
        {
            var paginatedResults = await _produtoRepository.ListarProdutos(request.Query, request.Page);

            var produtoViewModel = paginatedResults
                .Data
                .Select(p => new ProdutoViewModel(p.IdProduto, p.NomeProduto, p.Valor))
                .ToList();

            var paginationResult = new PaginationResult<ProdutoViewModel>(
               paginatedResults.Page,
               paginatedResults.TotalPages,
               paginatedResults.PageSize,
               paginatedResults.ItemsCount,
               produtoViewModel
               );
            return paginationResult;
        }
    }
}
