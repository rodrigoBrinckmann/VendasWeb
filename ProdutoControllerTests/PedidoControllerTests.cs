using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
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
    public class PedidoControllerTests
    {
        private readonly Mock<IPedidoService> _pedidoServiceMock = new();

        public PedidoControllerTests()
        {

        }
    

        [Fact(DisplayName = "PedidoControllerTests - Returns ok with a single order")]
        public async Task Get_ById()
        {
            //arrange            
            PedidoViewModel responsePedido = new PedidoViewModel(1,"Teste","email@email.com",true,10m,new List<ProdutoViewModel>());
            _pedidoServiceMock.Setup(s => s.ListarPedidoAsync(It.IsAny<int>())).ReturnsAsync(responsePedido);
            var controller = GetController();

            //act
            var response = await controller.GetOrderByIdAsync(1);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoControllerTests - Returns the list of orders")]
        public async Task Get_All()
        {
            //arrange
            List<PedidoViewModel> listPedidos = new List<PedidoViewModel>();            
            _pedidoServiceMock.Setup(s => s.ListarPedidosAsync()).ReturnsAsync(listPedidos);            
            var controller = GetController();

            //act
            var response = await controller.GetAllOrdersAsync();

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();            
        }

        [Fact(DisplayName = "PedidoControllerTests - Create a new order")]
        public async Task Post()
        {
            //arrange            
            Pedido pedido = new Pedido();
            _pedidoServiceMock.Setup(s => s.CadastrarPedidoAsync(pedido));
            var controller = GetController();

            //act
            var response = await controller.CadastrarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoControllerTests - Edit an order")]
        public async Task Put()
        {
            //arrange            
            Pedido pedido = new Pedido();
            _pedidoServiceMock.Setup(s => s.EditarPedidoAsync(It.IsAny<int>(), pedido)).ReturnsAsync(pedido);
            var controller = GetController();

            //act
            var response = await controller.EditarPedido(1, pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoControllerTests - Delete an order")]
        public async Task Delete()
        {
            //arrange            
            Pedido pedido = new Pedido();
            _pedidoServiceMock.Setup(s => s.DeletarPedidoAsync(It.IsAny<int>()));
            var controller = GetController();

            //act
            var response = await controller.DeletarPedido(1);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        private PedidoController GetController()
        {
            var controller = new PedidoController(_pedidoServiceMock.Object);
            return controller;
        }
    }
}
