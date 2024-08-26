using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebCore.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarProdutosAsync();
        Task<Produto> ListarProdutoAsync(int id);
        Task CadastrarProdutoAsync(Produto produto);
        Task<Produto> EditarProdutoAsync(int id, Produto produto);
        Task DeletarProdutoAsync(int id);
    }
}
