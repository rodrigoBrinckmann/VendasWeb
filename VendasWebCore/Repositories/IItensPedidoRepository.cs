using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;

namespace VendasWebCore.Repositories
{
    public interface IItensPedidoRepository
    {
        Task CadastrarItensPedidoAsync(List<ItensPedido> produto);
        Task<ItensPedido> EditarItensPedidoAsync(int id, ItensPedido itensPedido);
        Task DeletarItensPedido(int id);
        Task<PaginationResult<ItensPedido>> ListarItensPedido(string query, int page);
        Task<ItensPedido> ListarItensPedidoEspecífico(int id);
        Task SaveChangesASync();
    }    
}
