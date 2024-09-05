using VendasWebCore.Entities;
using VendasWebCore.Models;

namespace VendasWebCore.Repositories
{
    public interface IPedidoRepository
    {        
        Task<PaginationResult<Pedido>> ListarPedidos(string query, int page);
        Task<Pedido> ListarPedidoEspecífico(int identity);
        Task CadastrarPedidoAsync(Pedido produto);        
        Task<Pedido> RegistrarPagamentoPedidoAsync(int id, bool pago);
        Task DeletarPedido(int identity);        
        Task SaveChangesASync();
    }
}
