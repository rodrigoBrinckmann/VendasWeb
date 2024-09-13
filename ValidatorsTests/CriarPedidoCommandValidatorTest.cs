using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;
using VendasWebApplication.Validators;
using VendasWebCore.Entities;

namespace ValidatorsTests
{
    public class CriarPedidoCommandValidatorTest
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly IValidator<CriarPedidoCommand> _criarPedidoValidator = new CriarPedidoCommandValidator();
        private readonly Mock<IValidator<DeletarPedidoCommand>> _deletarPedidoValidatorMock = new();
        private readonly Mock<IValidator<RegistraPagamentoCommand>> _registraPagamentoValidatorMock = new();

        private readonly PedidoController _pedidoController;

        public CriarPedidoCommandValidatorTest()
        {
            _pedidoController = new PedidoController(_mediatrMock.Object, _criarPedidoValidator, _deletarPedidoValidatorMock.Object, _registraPagamentoValidatorMock.Object);
        }

        [Fact(DisplayName = "CriarPedidoCommandValidatorTest - User Id Validator")]
        public async Task UserId_NOK()
        {
            //arrange
            CriarPedidoCommand request = new() { Pago = true, DataCriacao = DateTime.Now, ItensPedidos = new List<ItensPedido>()};
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("UserId", new List<string>() { "Cliente é obrigatório" });

            //act
            var response = await _pedidoController.CadastrarPedido(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
