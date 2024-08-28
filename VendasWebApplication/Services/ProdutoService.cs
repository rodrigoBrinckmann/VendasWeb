using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApplication.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task CadastrarProdutoAsync(Produto produto)
        {
            await _produtoRepository.CadastrarProdutoAsync(produto);
        }

        public async Task DeletarProdutoAsync(int id)
        {
            await _produtoRepository.DeletarProduto(id);
        }

        public async Task<Produto> EditarProdutoAsync(int id, Produto produto)
        {
            return await _produtoRepository.EditarProdutoAsync(id, produto);
        }

        public async Task<Produto> ListarProdutoAsync(int id)
        {
            return await _produtoRepository.ListarProdutoEspecífico(id);
        }

        public async Task<List<Produto>> ListarProdutosAsync(string query)
        {
            return await _produtoRepository.ListarProdutos(query);            
        }
    }
}
