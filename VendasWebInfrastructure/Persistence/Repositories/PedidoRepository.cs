using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VendasWebCore.Calculation;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasWebDbContext _dbContext;

        public PedidoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
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
        }

        public async Task<Pedido> EditarPedidoAsync(int id, Pedido pedido)
        {
            try
            {
                var pedidoFinal = await _dbContext.Pedidos.SingleOrDefaultAsync(p => p.IdPedido == id);
                if (pedidoFinal is not null)
                {
                    pedidoFinal.Update(pedido);
                    await SaveChangesASync();
                    return pedidoFinal;
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Pedido não existente no banco de dados - Não atualizado", e);
            }
            return pedido;
        }

        public async Task<PedidoViewModel> ListarPedidoEspecífico(int idPedido)
        {
            var pedido = await _dbContext.Pedidos.SingleOrDefaultAsync(p => p.IdPedido == idPedido);

            if (pedido == null) return null;
            
            var itensDoPedido = await _dbContext.ItensPedidos.Where(p => p.IdPedido == idPedido).ToListAsync();

            List<ProdutoViewModel> produtosDoPedido = new();
            foreach (var item in itensDoPedido)
            {
                var produtoDoPedido = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == item.IdProduto);
                var produto = new ProdutoViewModel(item.IdPedido, produtoDoPedido.IdProduto, produtoDoPedido.NomeProduto, produtoDoPedido.Valor, item.Quantidade);                
                produtosDoPedido.Add(produto);
            }

            var valorTotal = Calculos.CalculaValorTotal(produtosDoPedido);
            

            var projetoDetalhadoViewModel = new PedidoViewModel(
                pedido.IdPedido,
                pedido.NomeCliente,
                pedido.EmailCliente,
                pedido.Pago,
                valorTotal,
                produtosDoPedido
                );

            return projetoDetalhadoViewModel;
        }

        public async Task<List<PedidoViewModel>> ListarPedidos(string query)
        {
            List<PedidoViewModel> todosPedidos = new();
            List<Pedido> pedidos = new();
            IQueryable<Pedido> pedidosQ = _dbContext.Pedidos;
            
            //usar filtragem = 'like'
            if (!string.IsNullOrWhiteSpace(query))
            {
                pedidosQ = pedidosQ
                    .Where(p =>
                    p.NomeCliente.Contains(query) ||
                    p.EmailCliente.Contains(query)
                    );
                pedidos = await pedidosQ.ToListAsync();
            }
            else
            {
                pedidos = await pedidosQ.ToListAsync();
            }
            
            if (pedidos == null) return null;
            foreach( var pedido in pedidos )
            {
                
                var itensDoPedido = await _dbContext.ItensPedidos.Where(p => p.IdPedido == pedido.IdPedido).ToListAsync();

                List<ProdutoViewModel> produtosDoPedido = new();
                foreach (var item in itensDoPedido)
                {
                    var produtoDoPedido = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == item.IdProduto);
                    var produto = new ProdutoViewModel(item.IdPedido, produtoDoPedido.IdProduto, produtoDoPedido.NomeProduto, produtoDoPedido.Valor, item.Quantidade);
                    produtosDoPedido.Add(produto);
                }
                var valorTotal = Calculos.CalculaValorTotal(produtosDoPedido);

                var projetoDetalhadoViewModel = new PedidoViewModel(
                pedido.IdPedido,
                pedido.NomeCliente,
                pedido.EmailCliente,
                pedido.Pago,
                valorTotal,
                produtosDoPedido
                );
                todosPedidos.Add(projetoDetalhadoViewModel);
            }
            


            return todosPedidos.ToList();
        }

        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
