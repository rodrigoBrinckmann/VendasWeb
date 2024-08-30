using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Services.ProdutoServices
{
    public class ProdutoService : IProdutoService
    {        
        private readonly IMediator _mediator;

        public ProdutoService(IMediator mediator)
        {
            _mediator = mediator;            
        }

        public async Task CadastrarProdutoAsync(CreateProdutoCommand produto)
        {
            await _mediator.Send(produto);            
        }

        public async Task DeletarProdutoAsync(DeleteProdutoCommand id)
        {
            await _mediator.Send(id);            
        }

        public async Task EditarProdutoAsync(UpdateProdutoCommand produto)
        {            
            await _mediator.Send(produto);            
        }

        public async Task<ProdutoViewModel> ListarProdutoAsync(GetProdutoByIdQuery query)
        {
            return await _mediator.Send(query);            
        }

        public async Task<PaginationResult<ProdutoViewModel>> ListarProdutosAsync(GetAllProdutosQuery produtosQuery)
        {
            return await _mediator.Send(produtosQuery);            
        }
    }
}
