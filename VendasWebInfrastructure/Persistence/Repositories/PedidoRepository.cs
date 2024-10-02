using Microsoft.EntityFrameworkCore;
using VendasWebCore.Calculation;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasWebDbContext _dbContext;
        private const int PAGE_SIZE = 5;

        public PedidoRepository(VendasWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<Pedido>> ListarPedidos(string query, int page)
        {            
            PaginationResult<Pedido> pedidos = new();

            IQueryable<Pedido> pedidosQ = _dbContext.Pedidos
                .Include(i => i.ItensPedidos)
                .ThenInclude(p => p.Produto)
                .ThenInclude(u => u.User);

            if (!string.IsNullOrWhiteSpace(query))
            {
                pedidosQ = pedidosQ
                    .Where(p =>
                    p.Cliente.FullName.Contains(query) ||
                    p.Cliente.Email.Contains(query)
                    );
                    

                pedidos = await pedidosQ.GetPaged<Pedido>(page, PAGE_SIZE);
            }
            else
            {
                pedidos = await pedidosQ.GetPaged<Pedido>(page, PAGE_SIZE);
            }

            if (pedidos == null) return null;

            return pedidos;            
        }
        

        public async Task<Pedido> ListarPedidoEspecífico(int idPedido)
        {
            var pedido = await _dbContext.Pedidos
                .Include(u => u.Cliente)
                .Include(i => i.ItensPedidos)
                    .ThenInclude(p => p.Produto)
                    .ThenInclude(u => u.User)
                .SingleOrDefaultAsync(p => p.IdPedido == idPedido);

            if (pedido == null) return null;
            return pedido;            
        }


        public async Task CadastrarPedidoAsync(Pedido pedido)
        {
            await _dbContext.Pedidos.AddAsync(pedido);
            await SaveChangesASync();
        }

        public async Task DeletarPedido(int id)
        {
            try
            {
                var pedidos = _dbContext.Pedidos.Attach(new Pedido { IdPedido = id });
                pedidos.State = EntityState.Deleted;
                await SaveChangesASync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException("Pedido não existente no banco de dados", e);
            }
            catch (Exception e)
            {
                throw new Exception("Este pedido não pode ser excluído", e);
            }
        }
        
        public async Task<Pedido> RegistrarPagamentoPedidoAsync(int id, bool pago)
        {
            try
            {
                var pedidoConsulta = await _dbContext.Pedidos.SingleOrDefaultAsync(p => p.IdPedido == id);
                if (pedidoConsulta is not null)
                {
                    pedidoConsulta.UpdatePagamento(pago);
                    await SaveChangesASync();
                    return pedidoConsulta;
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Pedido não existente no banco de dados - Não atualizado", e);
            }
            return new Pedido();
        }


        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
