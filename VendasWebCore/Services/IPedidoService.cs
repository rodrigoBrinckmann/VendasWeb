using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebCore.Services
{
    public interface IPedidoService
    {
        Task<PaginationResult<PedidoViewModel>> ListarPedidosAsync(string query, int page);
        Task<PedidoViewModel> ListarPedidoAsync(int id);
        Task CadastrarPedidoAsync(Pedido pedido);
        Task<Pedido> EditarPedidoAsync(int id, Pedido pedido);
        Task DeletarPedidoAsync(int id);
    }
}
