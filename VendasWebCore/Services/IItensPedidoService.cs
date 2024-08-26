using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;

namespace VendasWebCore.Services
{
    public interface IItensPedidoService
    {
        Task<List<ItensPedido>> ListarItensPedidosAsync();
        Task<ItensPedido> ListarItensPedidoAsync(int id);
        Task CadastrarItensPedidoAsync(ItensPedido itensPedido);
        Task<ItensPedido> EditarItensPedidoAsync(int id, ItensPedido itensPedido);
        Task DeletarItensPedidoAsync(int id);
    }
}
