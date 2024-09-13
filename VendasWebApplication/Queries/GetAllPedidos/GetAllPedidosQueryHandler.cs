using MediatR;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.Calculation;

namespace VendasWebApplication.Queries.GetAllPedidos
{
    public class GetAllPedidosQueryHandler : IRequestHandler<GetAllPedidosQuery, PaginationResult<PedidoViewModel>>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICalculos _calculos;

        public GetAllPedidosQueryHandler(IPedidoRepository pedidoRepository, ICalculos calculos)
        {
            _pedidoRepository = pedidoRepository;
            _calculos = calculos;
        }

        public async Task<PaginationResult<PedidoViewModel>> Handle(GetAllPedidosQuery request, CancellationToken cancellationToken)
        {
            List<PedidoViewModel> pedidosViewModel = new();
            var pedidos = await _pedidoRepository.ListarPedidos(request.Query, request.Page);

            foreach (var pedido in pedidos.Data)
            {                
                decimal valorTotal = 0m;
                List<ProdutoPedidoViewModel> produtosDoPedido = new();
                if (pedido.ItensPedidos != null)
                {
                    foreach (var item in pedido.ItensPedidos)
                    {                        
                        var produtoDoPedido = item.Produto;
                        var produto = new ProdutoPedidoViewModel(item.IdPedido, produtoDoPedido.IdProduto, produtoDoPedido.NomeProduto, produtoDoPedido.Valor, item.Quantidade);
                        produtosDoPedido.Add(produto);
                        valorTotal += _calculos.CalculaValorTotal(produto.Quantidade, produto.ValorUnitario);
                    }
                }

                var userViewModel = new UserViewModel(pedido.Cliente.FullName, pedido.Cliente.Email);

                var projetoDetalhadoViewModel = new PedidoViewModel(
                pedido.IdPedido,
                userViewModel,
                pedido.Pago,
                valorTotal,
                produtosDoPedido,
                pedido.DataCriacao
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
    }
}
