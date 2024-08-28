using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VendasWebApplication.Services;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace ServicesTests
{
    public class ProdutoServiceTests
    {

        private readonly Mock<IProdutoRepository> _produtoRepositoryMock = new();

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
            List<Produto> listaProdutos = new List<Produto>();
            _produtoRepositoryMock.Setup(s => s.ListarProdutos(It.IsAny<string>())).ReturnsAsync(listaProdutos);
            var service = GetService();
            //act
            var result = await service.ListarProdutosAsync("Teste");
            //assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ProdutoServiceTests - Cadastrar Produto")]
        public async Task Cadastrar_produto()
        {
            //arrange
            Produto produto = new Produto();
            _produtoRepositoryMock.Setup(s => s.CadastrarProdutoAsync(produto));
            var service = GetService();
            //act
            await service.CadastrarProdutoAsync(produto);
            //assert
            //no issues
        }

        [Fact(DisplayName = "ProdutoServiceTests - Deletar Produto")]
        public async Task Deletar_produto()
        {
            //arrange            
            _produtoRepositoryMock.Setup(s => s.DeletarProduto(1));
            var service = GetService();
            //act
            await service.DeletarProdutoAsync(1);
            //assert
            //no issues
        }

        [Fact(DisplayName = "ProdutoServiceTests - Editar Produto")]
        public async Task Editar_produto()
        {
            //arrange            
            Produto produto = new Produto();
            _produtoRepositoryMock.Setup(s => s.EditarProdutoAsync(1, produto));
            var service = GetService();
            //act
            await service.EditarProdutoAsync(1,produto);
            //assert
            //no issues
        }

        private ProdutoService GetService()
        {
            var service = new ProdutoService(_produtoRepositoryMock.Object);
            return service;
        }
    }
}