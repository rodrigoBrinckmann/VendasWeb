using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebApplication.ViewModels;
using VendasWebCore.Calculation;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Queries.GetPedidoById
{

    public class GetPedidoByIdQueryHandler : IRequestHandler<GetPedidoByIdQuery, PedidoViewModel>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidoByIdQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoViewModel> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ListarPedidoEspecífico(request.Id);
            if (pedido == null) return null;

            List <ProdutoPedidoViewModel> produtosDoPedido = new();
            decimal valorTotal = 0m;
            
            
            if (pedido.ItensPedidos is not null)
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

            return projetoDetalhadoViewModel;
        }
    }
}
