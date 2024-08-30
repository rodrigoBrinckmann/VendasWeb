using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebCore.Entities;
using VendasWebCore.Models;

namespace VendasWebApplication.Services.ProdutoServices
{
    public interface IProdutoService
    {
        Task<PaginationResult<Produto>> ListarProdutosAsync(string query, int page);
        Task<Produto> ListarProdutoAsync(int id);
        Task CadastrarProdutoAsync(CreateProdutoCommand produto);
        Task EditarProdutoAsync(UpdateProdutoCommand produto);
        Task DeletarProdutoAsync(DeleteProdutoCommand id);
    }
}