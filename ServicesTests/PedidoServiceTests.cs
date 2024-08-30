using FluentAssertions;
using Moq;
using VendasWebApplication.Services;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace ServicesTests
{
    public class PedidoServiceTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock = new();
        
        [Fact(DisplayName = "PedidoServiceTests - Listar pedido específico")]
        public async Task Listar_pedido_específico()
        {
            //arrange
            PedidoViewModel pedido = new PedidoViewModel(10,"TesteCliente","email@email.com",true,10m,new List<ProdutoPedidoViewModel>() { new ProdutoPedidoViewModel(1, 10, "Produto 123", 10m, 5) });
            _pedidoRepositoryMock.Setup(s => s.ListarPedidoEspecífico(It.IsAny<int>())).ReturnsAsync(pedido);
            var service = GetService();
            //act
            var result = await service.ListarPedidoAsync(1);
            //assert            
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoServiceTests - Listar todo pedidos")]
        public async Task Listar_todos_pedidos()
        {
            //arrange
            PaginationResult<PedidoViewModel> listaPedidos = new PaginationResult<PedidoViewModel>();
            _pedidoRepositoryMock.Setup(s => s.ListarPedidos(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(listaPedidos);
            var service = GetService();
            //act
            var result = await service.ListarPedidosAsync("Name123",5);
            //assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoServiceTests - Cadastrar Pedido")]
        public async Task Cadastrar_pedido()
        {
            //arrange
            Pedido pedido = new Pedido();
            _pedidoRepositoryMock.Setup(s => s.CadastrarPedidoAsync(pedido));
            var service = GetService();
            //act
            await service.CadastrarPedidoAsync(pedido);
            //assert
            //no issues
        }

        [Fact(DisplayName = "PedidoServiceTests - Deletar Pedido")]
        public async Task Deletar_pedido()
        {
            //arrange            
            _pedidoRepositoryMock.Setup(s => s.DeletarPedido(1));
            var service = GetService();
            //act
            await service.DeletarPedidoAsync(1);
            //assert
            //no issues
        }

        [Fact(DisplayName = "PedidoServiceTests - Editar Pedido")]
        public async Task Editar_pedido()
        {
            //arrange            
            Pedido pedido = new Pedido();
            _pedidoRepositoryMock.Setup(s => s.EditarPedidoAsync(1, pedido));
            var service = GetService();
            //act
            await service.EditarPedidoAsync(1, pedido);
            //assert
            //no issues
        }

        private PedidoService GetService()
        {
            var service = new PedidoService(_pedidoRepositoryMock.Object);
            return service;
        }
    }
}
