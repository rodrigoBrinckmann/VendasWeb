using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Services.ProdutoServices
{
    public interface IProdutoService
    {
        Task<PaginationResult<ProdutoViewModel>> ListarProdutosAsync(GetAllProdutosQuery produtosQuery);
        Task<ProdutoViewModel> ListarProdutoAsync(GetProdutoByIdQuery query);
        Task CadastrarProdutoAsync(CreateProdutoCommand produto);
        Task EditarProdutoAsync(UpdateProdutoCommand produto);
        Task DeletarProdutoAsync(DeleteProdutoCommand id);
    }
}