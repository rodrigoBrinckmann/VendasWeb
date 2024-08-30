using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Runtime.CompilerServices;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebApplication.Services.ProdutoServices;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;
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
            ProdutoViewModel responseProduto = new ProdutoViewModel();
            GetProdutoByIdQuery getProdutoByIdQuery = new GetProdutoByIdQuery();
            _produtoServiceMock.Setup(s => s.ListarProdutoAsync(It.IsAny<GetProdutoByIdQuery>())).ReturnsAsync(responseProduto);
            var controller = GetController();
            
            //act
            var response = await controller.GetProdutoByIdAsync(getProdutoByIdQuery);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }
                
        [Fact(DisplayName = "ProdutoControllerTests - Returns the list of products")]
        public async Task Get_All()
        {
            //arrange
            PaginationResult<ProdutoViewModel> listaProdutosResponse = new PaginationResult<ProdutoViewModel>();
            GetAllProdutosQuery getAllProdutosQuery = new GetAllProdutosQuery();
            _produtoServiceMock.Setup(s => s.ListarProdutosAsync(It.IsAny<GetAllProdutosQuery>())).ReturnsAsync(listaProdutosResponse);
            var controller = GetController();

            //act
            var response = await controller.GetAllProductsAsync(getAllProdutosQuery);

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
            CreateProdutoCommand produto = new CreateProdutoCommand("Produto1", 10m);
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
            UpdateProdutoCommand produto = new UpdateProdutoCommand(1,"Produto1",10m);
            _produtoServiceMock.Setup(s => s.EditarProdutoAsync(It.IsAny<UpdateProdutoCommand>()));
            var controller = GetController();

            //act
            var response = await controller.EditarProduto(produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoControllerTests - Delete a product")]
        public async Task Delete()
        {
            //arrange            
            DeleteProdutoCommand produto = new DeleteProdutoCommand(1);
            _produtoServiceMock.Setup(s => s.DeletarProdutoAsync(It.IsAny<DeleteProdutoCommand>()));
            var controller = GetController();

            //act
            var response = await controller.DeletarProduto(produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoControllerTests - Edit a product - Exception")]
        public async Task Put_NOK()
        {
            //arrange            
            UpdateProdutoCommand produto = new UpdateProdutoCommand(1,"Produto X", 7m);
            var expectedException = new KeyNotFoundException("Erro");
            _produtoServiceMock.Setup(s => s.EditarProdutoAsync(It.IsAny<UpdateProdutoCommand>())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.EditarProduto(produto);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "ProdutoControllerTests - Delete a product - Exception")]
        public async Task Delete_NOK()
        {
            //arrange            
            DeleteProdutoCommand produto = new DeleteProdutoCommand(1);
            var expectedException = new DbUpdateConcurrencyException("Erro");
            _produtoServiceMock.Setup(s => s.DeletarProdutoAsync(It.IsAny<DeleteProdutoCommand>())).ThrowsAsync(expectedException);
            var controller = GetController();

            //act
            var response = await controller.DeletarProduto(produto);

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