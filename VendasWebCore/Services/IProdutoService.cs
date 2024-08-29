using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;

namespace VendasWebCore.Services
{
    public interface IProdutoService
    {
        Task<PaginationResult<Produto>> ListarProdutosAsync(string query, int page);
        Task<Produto> ListarProdutoAsync(int id);
        Task CadastrarProdutoAsync(Produto produto);
        Task<Produto> EditarProdutoAsync(int id, Produto produto);
        Task DeletarProdutoAsync(int id);
    }
}
