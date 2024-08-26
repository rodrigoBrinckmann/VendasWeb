using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApplication.Services
{
    public class ItensPedidoService : IItensPedidoService
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;

        public ItensPedidoService(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task CadastrarItensPedidoAsync(ItensPedido itensPedido)
        {
            await _itensPedidoRepository.CadastrarItensPedidoAsync(itensPedido);
        }

        public async Task DeletarItensPedidoAsync(int id)
        {
            await _itensPedidoRepository.DeletarItensPedido(id);
        }

        public async Task<ItensPedido> EditarItensPedidoAsync(int id, ItensPedido itensPedido)
        {
            return await _itensPedidoRepository.EditarItensPedidoAsync(id, itensPedido);
        }

        public async Task<ItensPedido> ListarItensPedidoAsync(int id)
        {
            return await _itensPedidoRepository.ListarItensPedidoEspecífico(id);
        }

        public async Task<List<ItensPedido>> ListarAllItensPedidosAsync()
        {
            return await _itensPedidoRepository.ListarItensPedido();
        }
    }
}
