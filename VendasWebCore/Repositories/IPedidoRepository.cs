using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebApplication.ViewModels;

namespace VendasWebCore.Repositories
{
    public interface IPedidoRepository
    {
        Task CadastrarPedidoAsync(Pedido produto);
        Task<Pedido> EditarPedidoAsync(int id, Pedido produto);
        Task DeletarPedido(int identity);
        Task<List<PedidoViewModel>> ListarPedidos();
        Task<PedidoViewModel> ListarPedidoEspecífico(int identity);
        Task SaveChangesASync();
    }
}
