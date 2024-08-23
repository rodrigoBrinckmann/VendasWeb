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
        Task CadastrarProdutoASync(Produto produto);
        Task EditarProdutoAsync();
        Task<int> DeletarProduto();
        Task<List<Produto>> ListarProdutos();
        Task<Produto> ListarProdutoEspecífico(int id);
        Task SaveChangesASync(); //para quando comita no banco.
    }
}
