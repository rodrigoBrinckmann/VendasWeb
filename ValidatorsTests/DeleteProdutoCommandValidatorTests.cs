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
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;
using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class DeleteProdutoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateProdutoCommand>> _createProdutoCommandValidator = new ();

        private readonly IValidator<DeleteProdutoCommand> _deleteProdutoCommandValidator = new DeleteProdutoCommandValidator();
        private readonly Mock<IValidator<UpdateProdutoCommand>> _updateProdutoCommandValidator = new();

        private readonly ProdutoController _produtoController;

        public DeleteProdutoCommandValidatorTests()
        {
            _produtoController = new ProdutoController(_mediatrMock.Object, _createProdutoCommandValidator.Object, _deleteProdutoCommandValidator, _updateProdutoCommandValidator.Object);
        }

        [Fact(DisplayName = "DeleteProdutoCommandValidatorTests - Id obrigatório")]
        public async Task Id_empty_NOK()
        {
            //arrange
            DeleteProdutoCommand request = new DeleteProdutoCommand();
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Id", new List<string>() { "Id não pode ser nulo para deleção" });

            //act
            var response = await _produtoController.DeletarProduto(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
