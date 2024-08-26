using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebCore.Repositories
{
    public interface IProdutoRepository
    {
        Task CadastrarProdutoAsync(Produto produto);
        Task<Produto> EditarProdutoAsync(int id, Produto produto);
        Task DeletarProduto(int id);        
        Task<List<Produto>> ListarProdutos();
        Task<Produto> ListarProdutoEspecífico(int id);
        Task SaveChangesASync();
    }
}
