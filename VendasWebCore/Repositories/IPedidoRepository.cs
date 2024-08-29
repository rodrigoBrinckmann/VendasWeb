using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebCore.Repositories
{
    public interface IPedidoRepository
    {
        Task<PaginationResult<PedidoViewModel>> ListarPedidos(string query, int page);
        Task<PedidoViewModel> ListarPedidoEspecífico(int identity);
        Task CadastrarPedidoAsync(Pedido produto);
        Task<Pedido> EditarPedidoAsync(int id, Pedido produto);
        Task DeletarPedido(int identity);        
        Task SaveChangesASync();
    }
}
