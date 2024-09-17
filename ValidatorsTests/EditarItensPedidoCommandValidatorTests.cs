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
using VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido;
using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class EditarItensPedidoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<DeletarItensPedidoCommand>> _deletarItensPedidoCommandValidator = new();
        private readonly IValidator<EditarItensPedidoCommand> _editarItensPedidoCommandValidator = new EditarItensPedidoCommandValidator();

        private readonly ItensPedidoController _itensPedidoController;

        public EditarItensPedidoCommandValidatorTests()
        {
            _itensPedidoController = new ItensPedidoController(_mediatrMock.Object, _deletarItensPedidoCommandValidator.Object, _editarItensPedidoCommandValidator);
        }

        [Fact(DisplayName = "DeletarItensPedidoCommandValidatorTests - Id empty")]
        public async Task Id_empty_NOK()
        {
            //arrange
            EditarItensPedidoCommand request = new EditarItensPedidoCommand() { IdPedido = 1, IdProduto = 1, Quantidade = 12 };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Id", new List<string>() { "Id é obrigatório para edição" });

            //act
            var response = await _itensPedidoController.EditarItemPedido(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "DeletarItensPedidoCommandValidatorTests - IdPedido empty")]
        public async Task IdPedido_empty_NOK()
        {
            //arrange
            EditarItensPedidoCommand request = new EditarItensPedidoCommand() { Id = 1, IdProduto = 1, Quantidade = 12 };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("IdPedido", new List<string>() { "IdPedido é obrigatório para edição" });

            //act
            var response = await _itensPedidoController.EditarItemPedido(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "DeletarItensPedidoCommandValidatorTests - IdProduto empty")]
        public async Task IdProduto_empty_NOK()
        {
            //arrange
            EditarItensPedidoCommand request = new EditarItensPedidoCommand() { IdPedido = 1, Id = 1, Quantidade = 12 };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("IdProduto", new List<string>() { "IdProduto é obrigatório para edição" });

            //act
            var response = await _itensPedidoController.EditarItemPedido(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}