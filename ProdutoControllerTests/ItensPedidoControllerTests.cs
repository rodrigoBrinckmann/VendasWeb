using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApi.Controllers;
using VendasWebCore.Entities;
using VendasWebCore.Services;
using VendasWebCore.ViewModels;

namespace ControllerTests
{
    public class ItensPedidoControllerTests
    {
        private readonly Mock<IItensPedidoService> _itensPedidoServiceMock = new();

        public ItensPedidoControllerTests()
        {

        }


        [Fact(DisplayName = "ItensPedidoControllerTests - Returns ok with a single itemPedido")]
        public async Task Get_ById()
        {
            //arrange            
            ItensPedido itemPedido = new ItensPedido();
            _itensPedidoServiceMock.Setup(s => s.ListarItensPedidoAsync(It.IsAny<int>())).ReturnsAsync(itemPedido);
            var controller = GetController();

            //act
            var response = await controller.GetItensPedidoByIdAsync(1);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Returns the list of orders")]
        public async Task Get_All()
        {
            //arrange
            List<ItensPedido> listItensPedidos = new List<ItensPedido>();
            _itensPedidoServiceMock.Setup(s => s.ListarAllItensPedidosAsync()).ReturnsAsync(listItensPedidos);
            var controller = GetController();

            //act
            var response = await controller.GetAllItensPedidoAsync();

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Create a new itensPedido")]
        public async Task Post()
        {
            //arrange            
            ItensPedido itensPedido = new ItensPedido();
            _itensPedidoServiceMock.Setup(s => s.CadastrarItensPedidoAsync(itensPedido));
            var controller = GetController();

            //act
            var response = await controller.CadastrarItensPedido(itensPedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Editar ItemPedido")]
        public async Task Put()
        {
            //arrange            
            ItensPedido itensPedido = new ItensPedido() { IdPedido = 5};
            _itensPedidoServiceMock.Setup(s => s.EditarItensPedidoAsync(It.IsAny<int>(), itensPedido)).ReturnsAsync(itensPedido);
            var controller = GetController();

            //act
            var response = await controller.EditarItemPedido(1, itensPedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Deletar ItemPedido")]
        public async Task Delete()
        {
            //arrange                        
            _itensPedidoServiceMock.Setup(s => s.DeletarItensPedidoAsync(It.IsAny<int>()));
            var controller = GetController();

            //act
            var response = await controller.DeletarItemPedido(1);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Editar ItemPedido - Exception")]
        public async Task Put_NOK()
        {
            //arrange            
            ItensPedido itensPedido = new ItensPedido() { IdPedido = 0 };            
            _itensPedidoServiceMock.Setup(s => s.EditarItensPedidoAsync(It.IsAny<int>(), itensPedido)).ReturnsAsync(itensPedido);
            var controller = GetController();

            //act
            var response = await controller.EditarItemPedido(1, itensPedido);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Deletar ItemPedido - Exception")]
        public async Task Delete_NOK()
        {
            //arrange
            var expectedException = new DbUpdateConcurrencyException("Erro");
            _itensPedidoServiceMock.Setup(s => s.DeletarItensPedidoAsync(It.IsAny<int>())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.DeletarItemPedido(1);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        private ItensPedidoController GetController()
        {
            var controller = new ItensPedidoController(_itensPedidoServiceMock.Object);
            return controller;
        }
    }
}
