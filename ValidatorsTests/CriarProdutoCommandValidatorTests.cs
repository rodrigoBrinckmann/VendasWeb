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
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;
using VendasWebApplication.Commands.RetrievePasswordCommand;
using VendasWebApplication.Commands.UpdateUserCommand;

using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class CriarProdutoCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly IValidator<CreateProdutoCommand> _createProdutoCommandValidator = new CriarProdutoCommandValidator();

        private readonly Mock<IValidator<DeleteProdutoCommand>> _deleteProdutoCommandValidator = new();
        private readonly Mock<IValidator<UpdateProdutoCommand>> _updateProdutoCommandValidator = new();

        private readonly ProdutoController _produtoController;

        public CriarProdutoCommandValidatorTests()
        {
            _produtoController = new ProdutoController(_mediatrMock.Object, _createProdutoCommandValidator, _deleteProdutoCommandValidator.Object, _updateProdutoCommandValidator.Object);
        }

        [Fact(DisplayName = "CreateProdutoCommandValidatorTests - NomeProduto empty")]
        public async Task NomeProduto_empty_NOK()
        {
            //arrange
            CreateProdutoCommand request = new CreateProdutoCommand("", 10m);
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("NomeProduto", new List<string>() { "Nome do produto é obrigatório" });

            //act
            var response = await _produtoController.CadastrarProduto(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateProdutoCommandValidatorTests - NomeProduto oversized")]
        public async Task NomeProduto_oversized_NOK()
        {
            //arrange
            CreateProdutoCommand request = new CreateProdutoCommand("Produto oversized maior que 20 posições para validar", 10m);
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("NomeProduto", new List<string>() { "Nome do produto não pode exceder 20 posições" });

            //act
            var response = await _produtoController.CadastrarProduto(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateProdutoCommandValidatorTests - Valor empty")]
        public async Task Valor_empty_NOK()
        {
            //arrange
            CreateProdutoCommand request = new() { NomeProduto = "Produto"};
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Valor", new List<string>() { "Valor é obrigatório" });

            //act
            var response = await _produtoController.CadastrarProduto(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
