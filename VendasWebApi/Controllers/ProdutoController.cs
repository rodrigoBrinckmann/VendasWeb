using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.CreateProduto;
using VendasWebApplication.Commands.DeletarProduto;
using VendasWebApplication.Commands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
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
        /// <param name="getAllProductsQuery"></param>
        /// <remarks>
        /// https://localhost:7277/api/Produto
        /// 
        /// https://localhost:7277/api/Produto?query=****        
        /// </remarks>
        /// <returns>
        /// O parâmetro query e o page não são obrigatórios
        /// Se usado sem o parâmetro, vai trazer todos os produtos da base de dados
        /// Se colocado o parâmetro query, ele servirá como um filtro, trazendo todas as ocorrências de banco em que esse filtro apareça        
        /// Se colocado o parâmetro page, ele trará a pagina correspondente a consulta, no caso de haver mais de uma página
        /// </returns>        
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProdutosQuery getAllProductsQuery)
        {
            return Ok(await _produtoService.ListarProdutosAsync(getAllProductsQuery));
        }
                
        [HttpGet]
        [Route("produtoEspecifico")]
        public async Task<IActionResult> GetProdutoByIdAsync([FromQuery] GetProdutoByIdQuery query)
        {
            try
            {
                return Ok(await _produtoService.ListarProdutoAsync(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
