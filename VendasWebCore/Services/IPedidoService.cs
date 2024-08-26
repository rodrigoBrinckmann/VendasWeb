using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Entities;

namespace VendasWebCore.Services
{
    public interface IPedidoService
    {
        Task<List<PedidoViewModel>> ListarPedidosAsync();
        Task<PedidoViewModel> ListarPedidoAsync(int id);
        Task CadastrarPedidoAsync(Pedido pedido);
        Task<Pedido> EditarPedidoAsync(int id, Pedido pedido);
        Task DeletarPedidoAsync(int id);
    }
}
