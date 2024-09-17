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

namespace ValidatorsTests
{
    public class RegistraPagamentoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CriarPedidoCommand>> _criarPedidoValidator = new();
        private readonly Mock<IValidator<DeletarPedidoCommand>> _deletarPedidoValidatorMock = new();
        private readonly IValidator<RegistraPagamentoCommand> _registraPagamentoValidator = new RegistraPagamentoCommandValidator();

        private readonly PedidoController _pedidoController;

        public RegistraPagamentoCommandValidatorTests()
        {
            _pedidoController = new PedidoController(_mediatrMock.Object, _criarPedidoValidator.Object, _deletarPedidoValidatorMock.Object, _registraPagamentoValidator);
        }

        [Fact(DisplayName = "RegistraPagamentoCommandValidatorTests - Pedido Id Validator")]
        public async Task PedidoId_NOK()
        {
            //arrange
            RegistraPagamentoCommand request = new() { Pago = true};
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Id", new List<string>() { "É obrigatório fornecer o ID do pedido a ser editado" });

            //act
            var response = await _pedidoController.RegistrarPagamento(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
