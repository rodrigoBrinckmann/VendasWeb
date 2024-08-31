using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VendasWebCore.Calculation;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasWebDbContext _dbContext;
        private const int PAGE_SIZE = 5;

        public PedidoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<PedidoViewModel>> ListarPedidos(string query, int page)
        {
            List<PedidoViewModel> pedidosViewModel = new();
            PaginationResult<Pedido> pedidos = new();

            IQueryable<Pedido> pedidosQ = _dbContext.Pedidos;

            if (!string.IsNullOrWhiteSpace(query))
            {
                pedidosQ = pedidosQ
                    .Where(p =>
                    p.NomeCliente.Contains(query) ||
                    p.EmailCliente.Contains(query)
                    );
                pedidos = await pedidosQ.GetPaged<Pedido>(page, PAGE_SIZE);
            }
            else
            {
                pedidos = await pedidosQ.GetPaged<Pedido>(page, PAGE_SIZE);
            }

            if (pedidos == null) return null;

            foreach (var pedido in pedidos.Data)
            {

                var itensDoPedido = await _dbContext.ItensPedidos.Where(p => p.IdPedido == pedido.IdPedido).ToListAsync();

                List<ProdutoPedidoViewModel> produtosDoPedido = new();
                foreach (var item in itensDoPedido)
                {
                    var produtoDoPedido = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == item.IdProduto);
                    var produto = new ProdutoPedidoViewModel(item.IdPedido, produtoDoPedido.IdProduto, produtoDoPedido.NomeProduto, produtoDoPedido.Valor, item.Quantidade);
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
                pedidosViewModel.Add(projetoDetalhadoViewModel);
            }

            var paginationPedidosViewModel = new PaginationResult<PedidoViewModel>
                (
                    pedidos.Page,
                    pedidos.TotalPages,
                    pedidos.PageSize,
                    pedidos.ItemsCount,
                    pedidosViewModel
                );

            return paginationPedidosViewModel;
        }

        public async Task<PedidoViewModel> ListarPedidoEspecífico(int idPedido)
        {
            var pedido = await _dbContext.Pedidos.SingleOrDefaultAsync(p => p.IdPedido == idPedido);

            if (pedido == null) return null;

            var itensDoPedido = await _dbContext.ItensPedidos.Where(p => p.IdPedido == idPedido).ToListAsync();

            List<ProdutoPedidoViewModel> produtosDoPedido = new();
            foreach (var item in itensDoPedido)
            {
                var produtoDoPedido = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == item.IdProduto);
                var produto = new ProdutoPedidoViewModel(item.IdPedido, produtoDoPedido.IdProduto, produtoDoPedido.NomeProduto, produtoDoPedido.Valor, item.Quantidade);
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
                    pedidoFinal.Update(pedido, pedidoFinal);
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
