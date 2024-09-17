using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido;
using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class DeletarItensPedidoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly IValidator<DeletarItensPedidoCommand> _deletarItensPedidoCommandValidator = new DeletarItensPedidoCommandValidator();
        private readonly Mock<IValidator<EditarItensPedidoCommand>> _editarItensPedidoCommandValidator = new();

        private readonly ItensPedidoController _itensPedidoController;

        public DeletarItensPedidoCommandValidatorTests()
        {
            _itensPedidoController = new ItensPedidoController(_mediatrMock.Object, _deletarItensPedidoCommandValidator, _editarItensPedidoCommandValidator.Object);
        }

        [Fact(DisplayName = "DeletarItensPedidoCommandValidatorTests - Id empty")]
        public async Task Id_empty_NOK()
        {
            //arrange
            DeletarItensPedidoCommand request = new DeletarItensPedidoCommand();
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Id", new List<string>() { "Id é obrigatório para deleção" });

            //act
            var response = await _itensPedidoController.DeletarItemPedido(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
