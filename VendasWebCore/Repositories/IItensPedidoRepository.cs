using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebCore.Repositories
{
    public interface IItensPedidoRepository
    {
        Task CadastrarItensPedidoAsync(List<ItensPedido> produto);
        Task<ItensPedido> EditarItensPedidoAsync(int id, ItensPedido itensPedido);
        Task DeletarItensPedido(int id);
        Task<List<ItensPedido>> ListarItensPedido();
        Task<ItensPedido> ListarItensPedidoEspecífico(int id);
        Task SaveChangesASync();
    }    
}
