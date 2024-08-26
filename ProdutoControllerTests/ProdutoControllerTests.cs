using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Runtime.CompilerServices;
using VendasWebApi.Controllers;
using VendasWebCore.Entities;
using VendasWebCore.Services;
using Xunit;


namespace ProdutoControllerTests
{
    public class ProdutoControllerTests
    {        
        private readonly Mock<IProdutoService> _produtoServiceMock = new();

        public ProdutoControllerTests()
        {
            
        }
                
        [Fact(DisplayName = "ProdutoControllerTests - Returns ok with a single product")]
        public async Task Get_ById()
        {
            //arrange            
            Produto responseProduto = new Produto();            
            _produtoServiceMock.Setup(s => s.ListarProdutoAsync(It.IsAny<int>())).ReturnsAsync(responseProduto);
            var controller = GetController();
            
            //act
            var response = await controller.GetProdutoByIdAsync(1);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }
                
        [Fact(DisplayName = "ProdutoControllerTests - Returns the list of products")]
        public async Task Get_All()
        {
            //arrange
            List<Produto> listaProdutosResponse = new List<Produto>();
            _produtoServiceMock.Setup(s => s.ListarProdutosAsync()).ReturnsAsync(listaProdutosResponse);
            var controller = GetController();

            //act
            var response = await controller.GetAllProductsAsync();

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            //arrange
            
            //act
            //assert
        }

        [Fact(DisplayName = "ProdutoControllerTests - Create a new product")]
        public async Task Post()
        {
            //arrange            
            Produto produto = new Produto();
            _produtoServiceMock.Setup(s => s.CadastrarProdutoAsync(produto));
            var controller = GetController();

            //act
            var response = await controller.CadastrarProduto(produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoControllerTests - Edit a product")]
        public async Task Put()
        {
            //arrange            
            Produto produto = new Produto();
            _produtoServiceMock.Setup(s => s.EditarProdutoAsync(It.IsAny<int>(),produto)).ReturnsAsync(produto);
            var controller = GetController();

            //act
            var response = await controller.EditarProduto(1,produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoControllerTests - Delete a product")]
        public async Task Delete()
        {
            //arrange            
            Produto produto = new Produto();
            _produtoServiceMock.Setup(s => s.DeletarProdutoAsync(It.IsAny<int>()));
            var controller = GetController();

            //act
            var response = await controller.DeletarProduto(1);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoControllerTests - Edit a product - Exception")]
        public async Task Put_NOK()
        {
            //arrange            
            Produto produto = new Produto();
            var expectedException = new KeyNotFoundException("Erro");
            _produtoServiceMock.Setup(s => s.EditarProdutoAsync(It.IsAny<int>(), produto)).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.EditarProduto(1, produto);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "ProdutoControllerTests - Delete a product - Exception")]
        public async Task Delete_NOK()
        {
            //arrange            
            Produto produto = new Produto();
            var expectedException = new DbUpdateConcurrencyException("Erro");
            _produtoServiceMock.Setup(s => s.DeletarProdutoAsync(It.IsAny<int>())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.DeletarProduto(1);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        private ProdutoController GetController()
        {
            var controller = new ProdutoController(_produtoServiceMock.Object);
            return controller;
        }
    }
}