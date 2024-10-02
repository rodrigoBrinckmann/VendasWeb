using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;
using FluentValidation.Results;


namespace ControllersTests
{
    public class ProdutoControllerTests
    {           
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateProdutoCommand>> _createProductvalidatorMock = new();
        private readonly Mock<IValidator<DeleteProdutoCommand>> _deleteProductvalidatorMock = new();
        private readonly Mock<IValidator<UpdateProdutoCommand>> _updateProductvalidatorMock = new();


        [Fact(DisplayName = "ProdutoControllerTests - Get_ById - Returns ok with a single product")]
        public async Task Get_ById()
        {
            //arrange            
            ProdutoViewModel responseProduto = new ProdutoViewModel();
            GetProdutoByIdQuery getProdutoByIdQuery = new GetProdutoByIdQuery();            
            _mediatrMock.Setup(m => m.Send<ProdutoViewModel>(It.IsAny<GetProdutoByIdQuery>(), new CancellationToken())).ReturnsAsync(responseProduto);

            var controller = GetController();
            
            //act
            var response = await controller.GetProdutoByIdAsync(getProdutoByIdQuery);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<ProdutoViewModel>(It.IsAny<GetProdutoByIdQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoControllerTests - Get_ById_NotFound - Returns Not Found")]
        public async Task Get_ById_NotFound()
        {
            //arrange
            GetProdutoByIdQuery getProdutoByIdQuery = new GetProdutoByIdQuery();
            var expectedException = new Exception("Erro");
            _mediatrMock.Setup(m => m.Send<ProdutoViewModel>(It.IsAny<GetProdutoByIdQuery>(), new CancellationToken())).ThrowsAsync(expectedException);

            var controller = GetController();

            //act
            var response = await controller.GetProdutoByIdAsync(getProdutoByIdQuery);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<ProdutoViewModel>(It.IsAny<GetProdutoByIdQuery>(), new CancellationToken()), Times.Once());
        }


        [Fact(DisplayName = "ProdutoControllerTests - Get_All - Returns the list of products")]
        public async Task Get_All()
        {
            //arrange
            PaginationResult<ProdutoViewModel> listaProdutosResponse = new PaginationResult<ProdutoViewModel>();
            GetAllProdutosQuery getAllProdutosQuery = new GetAllProdutosQuery();            

            PaginationResult<ProdutoViewModel> listaProdutos = new PaginationResult<ProdutoViewModel>();            
            _mediatrMock.Setup(m => m.Send<PaginationResult<ProdutoViewModel>>(It.IsAny<GetAllProdutosQuery>(), new CancellationToken())).ReturnsAsync(listaProdutos);

            var controller = GetController();

            //act
            var response = await controller.GetAllProductsAsync(getAllProdutosQuery);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<PaginationResult<ProdutoViewModel>>(It.IsAny<GetAllProdutosQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoControllerTests - Post_CreateProduct - Create a new product")]
        public async Task Post_CreateProduct()
        {
            //arrange            
            CreateProdutoCommand produto = new CreateProdutoCommand("Produto1", 10m,1);
            ValidationResult vr = new();
            _createProductvalidatorMock.Setup(v => v.ValidateAsync(produto, new CancellationToken())).ReturnsAsync(vr);
            _mediatrMock.Setup(m => m.Send(It.IsAny<CreateProdutoCommand>(), new CancellationToken()));
                        
            var controller = GetController();

            //act
            var response = await controller.CadastrarProduto(produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<CreateProdutoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoControllerTests - Edit a product")]
        public async Task Put_EditProduct()
        {
            //arrange
            UpdateProdutoCommand produto = new UpdateProdutoCommand { Id = 1, NomeProduto = "Produto1", Valor = 10m };
            _mediatrMock.Setup(m => m.Send(It.IsAny<UpdateProdutoCommand>(), new CancellationToken()));
            ValidationResult vr = new();
            _updateProductvalidatorMock.Setup(v => v.ValidateAsync(produto, new CancellationToken())).ReturnsAsync(vr);

            var controller = GetController();

            //act
            var response = await controller.EditarProduto(produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<UpdateProdutoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoControllerTests - Delete a product")]
        public async Task Delete_Product()
        {
            //arrange            
            DeleteProdutoCommand produto = new DeleteProdutoCommand(1);
            ValidationResult vr = new();
            _deleteProductvalidatorMock.Setup(v => v.ValidateAsync(produto, new CancellationToken())).ReturnsAsync(vr);
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeleteProdutoCommand>(), new CancellationToken()));

            var controller = GetController();

            //act
            var response = await controller.DeletarProduto(produto);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send(It.IsAny<DeleteProdutoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoControllerTests - Edit a product - Exception")]
        public async Task Put_Edit_product_NOK()
        {
            //arrange            
            UpdateProdutoCommand produto = new UpdateProdutoCommand { Id = 1, NomeProduto = "Produto1", Valor = 10m };
            var expectedException = new KeyNotFoundException("Erro");
            _mediatrMock.Setup(m => m.Send(It.IsAny<UpdateProdutoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            ValidationResult vr = new();
            _updateProductvalidatorMock.Setup(v => v.ValidateAsync(produto, new CancellationToken())).ReturnsAsync(vr);

            var controller = GetController();

            //act
            var response = await controller.EditarProduto(produto);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "ProdutoControllerTests - Delete a product - Exception")]
        public async Task DeleteProduct_NOK()
        {
            //arrange
            DeleteProdutoCommand produto = new DeleteProdutoCommand(1);
            ValidationResult vr = new();
            _deleteProductvalidatorMock.Setup(v => v.ValidateAsync(produto, new CancellationToken())).ReturnsAsync(vr);
            var expectedException = new DbUpdateConcurrencyException("Erro");
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeleteProdutoCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            
            var controller = GetController();

            //act
            var response = await controller.DeletarProduto(produto);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        private ProdutoController GetController()
        {
            var controller = new ProdutoController(_mediatrMock.Object, _createProductvalidatorMock.Object, _deleteProductvalidatorMock.Object, _updateProductvalidatorMock.Object);
            return controller;
        }
    }
}