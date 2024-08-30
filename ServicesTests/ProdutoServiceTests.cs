using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Services.ProdutoServices;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.Repositories;

namespace ServicesTests
{
    public class ProdutoServiceTests
    {

        private readonly Mock<IProdutoRepository> _produtoRepositoryMock = new();
        private readonly Mock<IMediator> _mediatrMock = new();

        [Fact(DisplayName = "ProdutoServiceTests - Listar produto específico")]
        public async Task Listar_produto_específico()
        {
            //arrange
            Produto produto = new Produto();
            _produtoRepositoryMock.Setup(s => s.ListarProdutoEspecífico(It.IsAny<int>())).ReturnsAsync(produto);
            var service = GetService();
            //act
            var result = await service.ListarProdutoAsync(1);
            //assert            
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoServiceTests - Listar todo produtos")]
        public async Task Listar_todos_produtos()
        {
            //arrange
            PaginationResult<Produto> listaProdutos = new PaginationResult<Produto>();
            _produtoRepositoryMock.Setup(s => s.ListarProdutos(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(listaProdutos);
            var service = GetService();
            //act
            var result = await service.ListarProdutosAsync("Teste", 5);
            //assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoServiceTests - Cadastrar Produto")]
        public async Task Cadastrar_produto()
        {
            //arrange
            CreateProdutoCommand produtoCommand = new CreateProdutoCommand("Produto x", 15m);
            Produto produto = new Produto();
            _produtoRepositoryMock.Setup(s => s.CadastrarProdutoAsync(produto));
            var service = GetService();
            //act
            await service.CadastrarProdutoAsync(produtoCommand);
            //assert
            //no issues
        }

        [Fact(DisplayName = "ProdutoServiceTests - Deletar Produto")]
        public async Task Deletar_produto()
        {
            //arrange            
            DeleteProdutoCommand produtoCommand = new DeleteProdutoCommand(1);
            _produtoRepositoryMock.Setup(s => s.DeletarProduto(1));
            var service = GetService();
            //act
            await service.DeletarProdutoAsync(produtoCommand);
            //assert
            //no issues
        }

        [Fact(DisplayName = "ProdutoServiceTests - Editar Produto")]
        public async Task Editar_produto()
        {
            //arrange
            UpdateProdutoCommand produtoCommand = new UpdateProdutoCommand(1,"Produto x", 15m);
            Produto produto = new Produto();
            _produtoRepositoryMock.Setup(s => s.EditarProdutoAsync(1, produto));
            var service = GetService();
            //act
            await service.EditarProdutoAsync(produtoCommand);
            //assert
            //no issues
        }

        private ProdutoService GetService()
        {
            var service = new ProdutoService(_produtoRepositoryMock.Object, _mediatrMock.Object);
            return service;
        }
    }
}