using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetItemPedidoById
{
    public class GetItemPedidoByIdQueryHandler : IRequestHandler<GetItemPedidoByIdQuery, ItensPedidoViewModel>
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;
        public GetItemPedidoByIdQueryHandler(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task<ItensPedidoViewModel> Handle(GetItemPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            var itensPedido = await _itensPedidoRepository.ListarItensPedidoEspecífico(request.Id);
            var itensPedidoViewModel = new ItensPedidoViewModel()
            {
                IdPedido = itensPedido.IdPedido,
                IdProduto = itensPedido.IdProduto,
                Pedido = itensPedido.Pedido,
                Produto = itensPedido.Produto,
                Quantidade = itensPedido.Quantidade
            };


            return itensPedidoViewModel;
        }
    }
}
