using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebCore.Entities;
using VendasWebCore.Services;

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
                
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {         
            return Ok(await _produtoService.ListarProdutosAsync());
        }
                
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoByIdAsync(int id)
        {            
            return Ok(await _produtoService.ListarProdutoAsync(id));
        }
                
        [HttpPost]
        public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
        {
            await _produtoService.CadastrarProdutoAsync(produto);            
            return Ok("Produto Cadastrado com sucesso!");
        }
                
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto request)
        {
            try
            {                
                return Ok(await _produtoService.EditarProdutoAsync(id,request));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _produtoService.DeletarProdutoAsync(id);                
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);                
            }            
        }
    }
}
