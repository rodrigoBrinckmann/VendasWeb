using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApplication.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task CadastrarPedidoAsync(Pedido pedido)
        {
            await _pedidoRepository.CadastrarPedidoAsync(pedido);
        }

        public async Task DeletarPedidoAsync(int id)
        {
            await _pedidoRepository.DeletarPedido(id);
        }

        public async Task<Pedido> EditarPedidoAsync(int id, Pedido pedido)
        {
            return await _pedidoRepository.EditarPedidoAsync(id, pedido);
        }

        public async Task<PedidoViewModel> ListarPedidoAsync(int id)
        {
            return await _pedidoRepository.ListarPedidoEspecífico(id);
        }

        public async Task<List<PedidoViewModel>> ListarPedidosAsync()
        {
            return await _pedidoRepository.ListarPedidos();
        }
    }
}
