using FluentAssertions;
using FluentAssertions.Equivalency;
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
    public class UpdateProdutoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateProdutoCommand>> _createProdutoCommandValidator = new();

        private readonly Mock<IValidator<DeleteProdutoCommand>> _deleteProdutoCommandValidator = new();
        private readonly IValidator<UpdateProdutoCommand> _updateProdutoCommandValidator = new UpdateProdutoCommandValidator();

        private readonly ProdutoController _produtoController;

        public UpdateProdutoCommandValidatorTests()
        {
            _produtoController = new ProdutoController(_mediatrMock.Object, _createProdutoCommandValidator.Object, _deleteProdutoCommandValidator.Object, _updateProdutoCommandValidator);
        }

        [Fact(DisplayName = "UpdateProdutoCommandValidatorTests - Id obrigatório")]
        public async Task Id_empty_NOK()
        {
            //arrange
            UpdateProdutoCommand request = new UpdateProdutoCommand {NomeProduto = "Teste", Valor = 10m };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Id", new List<string>() { "É obrigatório fornecer o ID do produto a ser editado" });

            //act
            var response = await _produtoController.EditarProduto(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "UpdateProdutoCommandValidatorTests - NomeProduto não pode exceder o tamanho")]
        public async Task NomeDoProduto_Excede_tamanho_NOK()
        {
            //arrange
            UpdateProdutoCommand request = new UpdateProdutoCommand { NomeProduto = "Teste12345 Teste12345", Valor = 10m , Id = 1};
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("NomeProduto", new List<string>() { "Nome do produto não pode exceder 20 posições" });

            //act
            var response = await _produtoController.EditarProduto(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}