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
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Services.ProdutoServices
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediator _mediator;

        public ProdutoService(IProdutoRepository produtoRepository, IMediator mediator)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
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

        public async Task<Produto> ListarProdutoAsync(int id)
        {
            return await _produtoRepository.ListarProdutoEspecífico(id);
        }

        public async Task<PaginationResult<Produto>> ListarProdutosAsync(string query, int page)
        {
            return await _produtoRepository.ListarProdutos(query, page);
        }
    }
}
