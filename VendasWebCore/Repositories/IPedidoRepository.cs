using VendasWebCore.Entities;
using VendasWebCore.ViewModels;

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
