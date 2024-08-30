using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Services.ProdutoServices;
using VendasWebCore.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Lista todos os produtos. Pode usar um parâmetro adicional, de acordo com as instruções
        /// </summary>
        /// <param name="query"></param>
        /// <remarks>
        /// https://localhost:7277/api/Produto
        /// https://localhost:7277/api/Produto?query=****        
        /// </remarks>
        /// <returns>
        /// O parâmetro query não é obrigatório
        /// Se usado sem o parâmetro, vai trazer todos os produtos da base de dados
        /// Se colocado, ele servirá como um filtro, trazendo todas as ocorrências de banco em que esse filtro apareça        
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync(string? query, int page = 1)
        {         
            return Ok(await _produtoService.ListarProdutosAsync(query, page));
        }
                
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoByIdAsync(int id)
        {            
            return Ok(await _produtoService.ListarProdutoAsync(id));
        }
                
        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(CreateProdutoCommand request)
        {
            await _produtoService.CadastrarProdutoAsync(request);            
            return Ok("Produto Cadastrado com sucesso!");
        }
                
        [HttpPut]
        public async Task<IActionResult> EditarProduto(UpdateProdutoCommand request)
        {
            try
            {
                await _produtoService.EditarProdutoAsync(request);
                return Ok("Produto editado com sucesso");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(DeleteProdutoCommand request)
        {
            try
            {
                await _produtoService.DeletarProdutoAsync(request);
                return Ok("Produto deletado da base de dados");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);                
            }            
        }
    }
}
