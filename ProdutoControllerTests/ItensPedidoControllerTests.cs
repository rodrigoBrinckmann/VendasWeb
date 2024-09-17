using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ItensPedidoCommands.CadastrarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido;
using VendasWebApplication.Queries.GetAllItensPedidos;
using VendasWebApplication.Queries.GetItemPedidoById;
using VendasWebApplication.Validators;
using VendasWebApplication.ViewModels;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using FluentValidation.Results;

namespace ControllersTests
{
    public class ItensPedidoControllerTests
    {        
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<DeletarItensPedidoCommand>> _deletarItensPedidoCommandValidator = new();
        private readonly Mock<IValidator<EditarItensPedidoCommand>> _editarItensPedidoCommandValidator = new ();


        [Fact(DisplayName = "ItensPedidoControllerTests - Returns ok with a single itemPedido")]
        public async Task Get_ById()
        {
            //arrange            
            ItensPedidoViewModel itemPedido = new ItensPedidoViewModel();
            GetItemPedidoByIdQuery request = new GetItemPedidoByIdQuery();
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetItemPedidoByIdQuery>(), new CancellationToken())).ReturnsAsync(itemPedido);            
            var controller = GetController();

            //act
            var response = await controller.GetItensPedidoByIdAsync(request);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<ItensPedidoViewModel>(It.IsAny<GetItemPedidoByIdQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Returns Not Found")]
        public async Task Get_ById_NotFound()
        {
            //arrange
            GetItemPedidoByIdQuery request = new GetItemPedidoByIdQuery();
            var expectedException = new KeyNotFoundException("Erro");
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetItemPedidoByIdQuery>(), new CancellationToken())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.GetItensPedidoByIdAsync(request);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<ItensPedidoViewModel>(It.IsAny<GetItemPedidoByIdQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Returns the list of orders")]
        public async Task Get_All()
        {
            //arrange
            GetAllItensPedidosQuery listItensPedidos = new ();            
            PaginationResult<ItensPedidoViewModel> paginationItensPedidos = new();
            _mediatrMock.Setup(s => s.Send(It.IsAny<GetAllItensPedidosQuery>(), new CancellationToken())).ReturnsAsync(paginationItensPedidos);            
            var controller = GetController();

            //act
            var response = await controller.GetAllItensPedidoAsync(listItensPedidos);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<PaginationResult<ItensPedidoViewModel>>(It.IsAny<GetAllItensPedidosQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Create a new itensPedido")]
        public async Task Post()
        {
            //arrange            
            CadastrarItensPedidoCommand itensPedidoRequest = new();            
            _mediatrMock.Setup(s => s.Send(It.IsAny<CadastrarItensPedidoCommand>(), new CancellationToken()));
            var controller = GetController();

            //act
            var response = await controller.CadastrarItensPedido(itensPedidoRequest);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<Unit>(It.IsAny<CadastrarItensPedidoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Editar ItemPedido")]
        public async Task Put()
        {
            //arrange            
            ItensPedido itensPedido = new ItensPedido() { IdPedido = 5};
            EditarItensPedidoCommand command = new();
            _mediatrMock.Setup(s => s.Send(It.IsAny<EditarItensPedidoCommand>(), new CancellationToken())).ReturnsAsync(itensPedido);
            ValidationResult vr = new();
            _editarItensPedidoCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);
            var controller = GetController();

            //act
            var response = await controller.EditarItemPedido(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<ItensPedido>(It.IsAny<EditarItensPedidoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Deletar ItemPedido")]
        public async Task Delete()
        {
            //arrange            
            DeletarItensPedidoCommand command = new();
            _mediatrMock.Setup(s => s.Send(It.IsAny<DeletarItensPedidoCommand>(), new CancellationToken()));
            ValidationResult vr = new();
            _deletarItensPedidoCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            var controller = GetController();

            //act
            var response = await controller.DeletarItemPedido(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<Unit>(It.IsAny<DeletarItensPedidoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Editar ItemPedido - Exception")]
        public async Task Put_NOK()
        {
            //arrange            
            ItensPedido itensPedido = new ItensPedido() { IdPedido = 0 };
            EditarItensPedidoCommand command = new();
            ValidationResult vr = new();
            _editarItensPedidoCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);
            _mediatrMock.Setup(s => s.Send(It.IsAny<EditarItensPedidoCommand>(), new CancellationToken())).ReturnsAsync(itensPedido);
            var controller = GetController();

            //act
            var response = await controller.EditarItemPedido(command);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "ItensPedidoControllerTests - Deletar ItemPedido - Exception")]
        public async Task Delete_NOK()
        {
            //arrange
            DeletarItensPedidoCommand command = new();
            var expectedException = new DbUpdateConcurrencyException("Erro");
            _mediatrMock.Setup(s => s.Send(It.IsAny<DeletarItensPedidoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            ValidationResult vr = new();
            _deletarItensPedidoCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);
            var controller = GetController();

            //act
            var response = await controller.DeletarItemPedido(command);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        private ItensPedidoController GetController()
        {
            var controller = new ItensPedidoController(_mediatrMock.Object, _deletarItensPedidoCommandValidator.Object, _editarItensPedidoCommandValidator.Object);
            return controller;
        }
    }
}
