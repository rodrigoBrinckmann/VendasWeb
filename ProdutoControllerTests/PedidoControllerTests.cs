using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;
using VendasWebApplication.Queries.GetAllPedidos;
using VendasWebApplication.Queries.GetPedidoById;
using VendasWebApplication.ViewModels;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using FluentValidation.Results;

namespace ControllersTests
{
    public class PedidoControllerTests
    {        
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CriarPedidoCommand>> _criarPedidoValidatorMock = new();
        private readonly Mock<IValidator<DeletarPedidoCommand>> _deletarPedidoValidatorMock = new();
        private readonly Mock<IValidator<RegistraPagamentoCommand>> _registraPagamentoValidatorMock = new();


        [Fact(DisplayName = "PedidoControllerTests - Returns ok with a single order")]
        public async Task Get_ById()
        {            
            //arrange            
            UserViewModel user = new UserViewModel("Teste","Test@mail.com");
            PedidoViewModel responsePedido = new PedidoViewModel(1, user, true,10m,new List<ProdutoPedidoViewModel>(),DateTime.Now);
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetPedidoByIdQuery>(), new CancellationToken())).ReturnsAsync(responsePedido);            
            var controller = GetController();

            //act
            var response = await controller.GetOrderByIdAsync(new GetPedidoByIdQuery());

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "PedidoControllerTests - Pedido doesn't exists")]
        public async Task Get_ById_Doesnt_exists()
        {
            //arrange            
            UserViewModel user = new UserViewModel("Teste", "Test@mail.com");
            PedidoViewModel responsePedido = new PedidoViewModel(1, user, true, 10m, new List<ProdutoPedidoViewModel>(), DateTime.Now);
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
            ValidationResult vr = new();
            _criarPedidoValidatorMock.Setup(v => v.ValidateAsync(pedido, new CancellationToken())).ReturnsAsync(vr);                      
            _mediatrMock.Setup(m => m.Send(It.IsAny<CriarPedidoCommand>(), new CancellationToken())).ReturnsAsync(It.IsAny<int>);
            
            var controller = GetController();

            //act
            var response = await controller.CadastrarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<CriarPedidoCommand>(), new CancellationToken()), Times.Once());
        }
        
        [Fact(DisplayName = "PedidoControllerTests - Register a payment")]
        public async Task RegisterPayment()
        {
            //arrange
            RegistraPagamentoCommand pedido = new RegistraPagamentoCommand();
            ValidationResult vr = new();
            _registraPagamentoValidatorMock.Setup(v => v.ValidateAsync(pedido, new CancellationToken())).ReturnsAsync(vr);
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
            ValidationResult vr = new();
            _deletarPedidoValidatorMock.Setup(v => v.ValidateAsync(pedido, new CancellationToken())).ReturnsAsync(vr);
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken()));
            var controller = GetController();

            //act
            var response = await controller.DeletarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken()), Times.Once());
        }
        
        [Fact(DisplayName = "PedidoControllerTests - Register Payment - Exception")]
        public async Task Put_RegistraPagamento_NOK()
        {
            //arrange            
            RegistraPagamentoCommand pedido = new RegistraPagamentoCommand();
            ValidationResult vr = new();
            _registraPagamentoValidatorMock.Setup(v => v.ValidateAsync(pedido, new CancellationToken())).ReturnsAsync(vr);
            var expectedException = new KeyNotFoundException("Erro");
            _mediatrMock.Setup(m => m.Send(It.IsAny<RegistraPagamentoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);

            var controller = GetController();

            //act
            var response = await controller.RegistrarPagamento(pedido);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "PedidoControllerTests - Delete an order - DbUpdateConcurrencyException")]
        public async Task Delete_NOK_DbUpdateConcurrencyException()
        {
            //arrange            
            DeletarPedidoCommand pedido = new DeletarPedidoCommand();
            ValidationResult vr = new();
            _deletarPedidoValidatorMock.Setup(v => v.ValidateAsync(pedido, new CancellationToken())).ReturnsAsync(vr);
            var expectedException = new DbUpdateConcurrencyException("Erro");            
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.DeletarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "PedidoControllerTests - Delete an order - Exception")]
        public async Task Delete_NOK_Exception()
        {
            //arrange            
            DeletarPedidoCommand pedido = new DeletarPedidoCommand();
            ValidationResult vr = new();
            _deletarPedidoValidatorMock.Setup(v => v.ValidateAsync(pedido, new CancellationToken())).ReturnsAsync(vr);
            var expectedException = new Exception("Erro");
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeletarPedidoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.DeletarPedido(pedido);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        private PedidoController GetController()
        {
            var controller = new PedidoController(_mediatrMock.Object, _criarPedidoValidatorMock.Object, _deletarPedidoValidatorMock.Object,_registraPagamentoValidatorMock.Object);
            return controller;
        }
    }
}
