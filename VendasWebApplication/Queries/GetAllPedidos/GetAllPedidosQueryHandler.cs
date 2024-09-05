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
        
        public GetAllPedidosQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
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
                        valorTotal += Calculos.CalculaValorTotal(produto.Quantidade, produto.ValorUnitario);
                    }
                }

                var projetoDetalhadoViewModel = new PedidoViewModel(
                pedido.IdPedido,
                pedido.NomeCliente,
                pedido.EmailCliente,
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
