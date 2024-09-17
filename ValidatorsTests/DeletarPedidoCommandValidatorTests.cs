using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;
using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class DeletarPedidoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CriarPedidoCommand>> _criarPedidoValidator = new ();
        private readonly IValidator<DeletarPedidoCommand> _deletarPedidoValidator = new DeletarPedidoCommandValidator();
        private readonly Mock<IValidator<RegistraPagamentoCommand>> _registraPagamentoValidatorMock = new();

        private readonly PedidoController _pedidoController;

        public DeletarPedidoCommandValidatorTests()
        {
            _pedidoController = new PedidoController(_mediatrMock.Object, _criarPedidoValidator.Object, _deletarPedidoValidator, _registraPagamentoValidatorMock.Object);
        }

        [Fact(DisplayName = "DeletarPedidoCommandValidatorTests - Id obrigatório")]
        public async Task Id_empty_NOK()
        {
            //arrange
            DeletarPedidoCommand request = new DeletarPedidoCommand();
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Id", new List<string>() { "Id é obrigatório para deleção" });

            //act
            var response = await _pedidoController.DeletarPedido(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
