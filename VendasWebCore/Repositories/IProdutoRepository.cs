using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebCore.Repositories
{
    public interface IProdutoRepository
    {
        Task<PaginationResult<Produto>> ListarProdutos(string query, int page);
        Task<Produto> ListarProdutoEspecífico(int id);
        Task CadastrarProdutoAsync(Produto produto);
        Task EditarProdutoAsync(int id, Produto produto);
        Task DeletarProduto(int id);
        Task SaveChangesASync();
    }
}
