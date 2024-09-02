using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class ItensPedidoRepository : IItensPedidoRepository
    {
        private readonly VendasWebDbContext _dbContext;
        private const int PAGE_SIZE = 5;

        public ItensPedidoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<ItensPedido>> ListarItensPedido(string query, int page)
        {
            IQueryable<ItensPedido> itensPedido = _dbContext.ItensPedidos
                .Include(pp => pp.Produto)
                .Include(pp => pp.Pedido);
            if (!string.IsNullOrWhiteSpace(query))
            {
                itensPedido = itensPedido
                    .Where(p =>
                    p.Produto.NomeProduto.Contains(query));
            }

            return await itensPedido.GetPaged<ItensPedido>(page, PAGE_SIZE);            
        }

        public async Task<ItensPedido> ListarItensPedidoEspecífico(int id)
        {
            try
            {
                var itemPedido = await _dbContext.ItensPedidos
                    .Include(pp => pp.Produto)
                    .Include(pp => pp.Pedido)
                    .FirstOrDefaultAsync(ip => ip.ItemPedidoId == id);
                    
                if (itemPedido is not null)
                {
                    return itemPedido;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("ItemPedido não existente no banco de dados");
            }            
        }

        public async Task CadastrarItensPedidoAsync(List<ItensPedido> itensPedidoList)
        {
            foreach (var itemPedido in itensPedidoList)
                await _dbContext.ItensPedidos.AddAsync(itemPedido);
            await SaveChangesASync();
        }

        public async Task DeletarItensPedido(int id)
        {
            try
            {
                var itensPedido = _dbContext.ItensPedidos.Attach(new ItensPedido { ItemPedidoId = id });
                itensPedido.State = EntityState.Deleted;
                await SaveChangesASync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException("ItensPedido não existente no banco de dados", e);
            }
        }

        public async Task<ItensPedido> EditarItensPedidoAsync(int id, ItensPedido itensPedido)        
        {            
            var itensPedidoFinal = await _dbContext.ItensPedidos.SingleOrDefaultAsync(p => p.ItemPedidoId == id);
            if (itensPedidoFinal is not null)
            {
                itensPedidoFinal.Update(itensPedido);
                await SaveChangesASync();
                return itensPedido;
            }
            return itensPedido;
        }        

        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
