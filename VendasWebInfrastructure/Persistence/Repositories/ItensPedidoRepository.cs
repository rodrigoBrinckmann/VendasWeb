using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class ItensPedidoRepository : IItensPedidoRepository
    {
        private readonly VendasWebDbContext _dbContext;

        public ItensPedidoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ItensPedido>> ListarItensPedido()
        {
            return await _dbContext.ItensPedidos.ToListAsync();
        }

        public async Task<ItensPedido> ListarItensPedidoEspecífico(int id)
        {
            var itemPedido = await _dbContext.ItensPedidos.FirstOrDefaultAsync(ip => ip.ItemPedidoId == id);
            return itemPedido;
        }

        public async Task CadastrarItensPedidoAsync(ItensPedido itensPedido)
        {
            await _dbContext.ItensPedidos.AddAsync(itensPedido);
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
