using VendasWebCore.Entities;
using VendasWebCore.ViewModels;

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
