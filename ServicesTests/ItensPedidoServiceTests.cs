using FluentAssertions;
using Moq;
using VendasWebApplication.Services;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace ServicesTests
{
    public class ItensPedidoServiceTests
    {
        private readonly Mock<IItensPedidoRepository> _itensPedidoRepositoryMock = new();

        [Fact(DisplayName = "ItensPedidoServiceTests - Listar itensPedido específico")]
        public async Task Listar_itensPedido_específico()
        {
            //arrange
            ItensPedido itensPedido = new ItensPedido();
            _itensPedidoRepositoryMock.Setup(s => s.ListarItensPedidoEspecífico(It.IsAny<int>())).ReturnsAsync(itensPedido);
            var service = GetService();
            //act
            var result = await service.ListarItensPedidoAsync(1);
            //assert            
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoServiceTests - Listar todo itensPedidos")]
        public async Task Listar_todos_itensPedidos()
        {
            //arrange
            List<ItensPedido> listaItensPedido = new List<ItensPedido>();
            _itensPedidoRepositoryMock.Setup(s => s.ListarItensPedido()).ReturnsAsync(listaItensPedido);
            var service = GetService();
            //act
            var result = await service.ListarAllItensPedidosAsync();
            //assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoServiceTests - Cadastrar ItensPedido")]
        public async Task Cadastrar_itensPedido()
        {
            //arrange
            ItensPedido itensPedido = new ItensPedido();
            _itensPedidoRepositoryMock.Setup(s => s.CadastrarItensPedidoAsync(itensPedido));
            var service = GetService();
            //act
            await service.CadastrarItensPedidoAsync(itensPedido);
            //assert
            //no issues
        }

        [Fact(DisplayName = "ItensPedidoServiceTests - Deletar ItensPedido")]
        public async Task Deletar_itensPedido()
        {
            //arrange            
            _itensPedidoRepositoryMock.Setup(s => s.DeletarItensPedido(1));
            var service = GetService();
            //act
            await service.DeletarItensPedidoAsync(1);
            //assert
            //no issues
        }

        [Fact(DisplayName = "ItensPedidoServiceTests - Editar ItensPedido")]
        public async Task Editar_itensPedido()
        {
            //arrange            
            ItensPedido itensPedido = new ItensPedido();
            _itensPedidoRepositoryMock.Setup(s => s.EditarItensPedidoAsync(1, itensPedido));
            var service = GetService();
            //act
            await service.EditarItensPedidoAsync(1, itensPedido);
            //assert
            //no issues
        }

        private ItensPedidoService GetService()
        {
            var service = new ItensPedidoService(_itensPedidoRepositoryMock.Object);
            return service;
        }
    }
}
