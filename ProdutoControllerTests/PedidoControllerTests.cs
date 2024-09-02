using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;
using VendasWebApplication.Commands.PedidoCommands.EditarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;
using VendasWebApplication.Queries.GetAllPedidos;
using VendasWebApplication.Queries.GetPedidoById;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace ControllerTests
{
    public class PedidoControllerTests
    {        
        private readonly Mock<IMediator> _mediatrMock = new();
    

        [Fact(DisplayName = "PedidoControllerTests - Returns ok with a single order")]
        public async Task Get_ById()
        {
            //arrange            
            PedidoViewModel responsePedido = new PedidoViewModel(1,"Teste","email@email.com",true,10m,new List<ProdutoPedidoViewModel>(),DateTime.Now);
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetPedidoByIdQuery>(), new CancellationToken())).ReturnsAsync(responsePedido);            
            var controller = GetController();

            //act
            var response = await controller.GetOrderByIdAsync(new GetPedidoByIdQuery());

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoControllerTests - Pedido doesn' exists")]
        public async Task Get_ById_Doesnt_exists()
        {
            //arrange            
            PedidoViewModel responsePedido = new PedidoViewModel(1, "Teste", "email@email.com", true, 10m, new List<ProdutoPedidoViewModel>(), DateTime.Now);
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetPedidoByIdQuery>(), new CancellationToken()));
            var controller = GetController();

            //act
            var response = await controller.GetOrderByIdAsync(new GetPedidoByIdQuery());

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().Be("Pedido não cadastrado na base de dados");
        }

        [Fact(DisplayName = "PedidoControllerTests - Returns the list of orders")]
        public async Task Get_All()
        {
            //arrange
            PaginationResult<PedidoViewModel> listPedidos = new PaginationResult<PedidoViewModel>();
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetAllPedidosQuery>(), new CancellationToken())).ReturnsAsync(listPedidos);                    
            var controller = GetController();

            //act
            var response = await controller.GetAllOrdersAsync(new GetAllPedidosQuery());

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();            
        }

        [Fact(DisplayName = "PedidoControllerTests - Create a new order")]
        public async Task Post()
        {
            //arrange            
            CriarPedidoCommand pedido = new CriarPedidoCommand();            
            _mediatrMock.Setup(m => m.Send(It.IsAny<CriarPedidoCommand>(), new CancellationToken()));
            
            var controller = GetController();

            //act
            var response = await controller.CadastrarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<CriarPedidoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "PedidoControllerTests - Edit an order")]
        public async Task Put()
        {
            //arrange            
            EditarPedidoCommand pedido = new EditarPedidoCommand();
            var responseProduto = new Pedido();
            _mediatrMock.Setup(m => m.Send(It.IsAny<EditarPedidoCommand>(), new CancellationToken())).ReturnsAsync(responseProduto);
            var controller = GetController();

            //act
            var response = await controller.EditarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<EditarPedidoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "PedidoControllerTests - Register a payment")]
        public async Task RegisterPayment()
        {
            //arrange
            RegistraPagamentoCommand pedido = new RegistraPagamentoCommand();
            var responseProduto = new Pedido();
            _mediatrMock.Setup(m => m.Send(It.IsAny<RegistraPagamentoCommand>(), new CancellationToken())).ReturnsAsync(responseProduto);
            var controller = GetController();

            //act
            var response = await controller.RegistrarPagamento(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<RegistraPagamentoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "PedidoControllerTests - Delete an order")]
        public async Task Delete()
        {
            //arrange            
            DeletarPedidoCommand pedido = new DeletarPedidoCommand();
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken()));
            var controller = GetController();

            //act
            var response = await controller.DeletarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "PedidoControllerTests - Edit an order - Exception")]
        public async Task Put_NOK()
        {
            //arrange            
            EditarPedidoCommand pedido = new EditarPedidoCommand();
            var expectedException = new KeyNotFoundException("Erro");
            _mediatrMock.Setup(m => m.Send(It.IsAny<EditarPedidoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            
            var controller = GetController();

            //act
            var response = await controller.EditarPedido(pedido);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "PedidoControllerTests - Register Payment - Exception")]
        public async Task Put_RegistraPagamento_NOK()
        {
            //arrange            
            RegistraPagamentoCommand pedido = new RegistraPagamentoCommand();
            var expectedException = new KeyNotFoundException("Erro");
            _mediatrMock.Setup(m => m.Send(It.IsAny<RegistraPagamentoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);

            var controller = GetController();

            //act
            var response = await controller.RegistrarPagamento(pedido);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "PedidoControllerTests - Delete an order - Exception")]
        public async Task Delete_NOK()
        {
            //arrange            
            DeletarPedidoCommand pedido = new DeletarPedidoCommand();
            var expectedException = new DbUpdateConcurrencyException("Erro");            
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.DeletarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        private PedidoController GetController()
        {
            var controller = new PedidoController(_mediatrMock.Object);
            return controller;
        }
    }
}
