using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebApplication.Services.ProdutoServices;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;
using VendasWebCore.ViewModels;

namespace ServicesTests
{
    public class ProdutoServiceTests
    {        
        private readonly Mock<IMediator> _mediatrMock = new();

        [Fact(DisplayName = "ProdutoServiceTests - Listar produto específico")]
        public async Task Listar_produto_específico()
        {
            //arrange            
            ProdutoViewModel produtoRequest = new ProdutoViewModel();
            GetProdutoByIdQuery query = new GetProdutoByIdQuery();            

            _mediatrMock.Setup(m => m.Send<ProdutoViewModel>(It.IsAny<GetProdutoByIdQuery>(), new CancellationToken())).ReturnsAsync(produtoRequest);  
            var service = GetService();
            //act
            var result = await service.ListarProdutoAsync(query);
            //assert            
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoServiceTests - Listar todo produtos")]
        public async Task Listar_todos_produtos()
        {
            //arrange
            PaginationResult<ProdutoViewModel> listaProdutos = new PaginationResult<ProdutoViewModel>();
            GetAllProdutosQuery query = new GetAllProdutosQuery();
            _mediatrMock.Setup(m => m.Send<PaginationResult<ProdutoViewModel>>(It.IsAny<GetAllProdutosQuery>(), new CancellationToken())).ReturnsAsync(listaProdutos);
                        
            var service = GetService();
            //act
            var result = await service.ListarProdutosAsync(query);
            //assert
            result.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<PaginationResult<ProdutoViewModel>>(It.IsAny<GetAllProdutosQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoServiceTests - Cadastrar Produto")]
        public async Task Cadastrar_produto()
        {
            //arrange
            CreateProdutoCommand produtoCommand = new CreateProdutoCommand("Produto x", 15m);            
            _mediatrMock.Setup(m => m.Send(It.IsAny<CreateProdutoCommand>(), new CancellationToken()));
            
            var service = GetService();
            //act
            await service.CadastrarProdutoAsync(produtoCommand);
            //assert
            _mediatrMock.Verify(x => x.Send(It.IsAny<CreateProdutoCommand>(), new CancellationToken()), Times.Once());            
        }

        [Fact(DisplayName = "ProdutoServiceTests - Deletar Produto")]
        public async Task Deletar_produto()
        {
            //arrange            
            DeleteProdutoCommand produtoCommand = new DeleteProdutoCommand(1);            
            _mediatrMock.Setup(m => m.Send(It.IsAny<DeleteProdutoCommand>(), new CancellationToken()));
            var service = GetService();
            //act
            await service.DeletarProdutoAsync(produtoCommand);
            //assert
            _mediatrMock.Verify(x => x.Send(It.IsAny<DeleteProdutoCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "ProdutoServiceTests - Editar Produto")]
        public async Task Editar_produto()
        {
            //arrange
            UpdateProdutoCommand produtoCommand = new UpdateProdutoCommand(1,"Produto x", 15m);            
            _mediatrMock.Setup(m => m.Send(It.IsAny<UpdateProdutoCommand>(), new CancellationToken()));
            var service = GetService();
            //act
            await service.EditarProdutoAsync(produtoCommand);
            //assert
            _mediatrMock.Verify(x => x.Send(It.IsAny<UpdateProdutoCommand>(), new CancellationToken()), Times.Once());
        }

        private ProdutoService GetService()
        {
            var service = new ProdutoService(_mediatrMock.Object);
            return service;
        }
    }
}