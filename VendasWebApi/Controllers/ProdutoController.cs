using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendasWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ProdutoController : ControllerBase
    {        
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todos os produtos. Pode usar um parâmetro adicional, de acordo com as instruções
        /// </summary>
        /// <param name="getAllProductsQuery"></param>
        /// <remarks>
        /// https://localhost:7277/api/Produto
        /// 
        /// https://localhost:7277/api/Produto?Query=12321&Page=1231****
        /// </remarks>
        /// <returns>
        /// O parâmetro query e o page não são obrigatórios
        /// Se usado sem o parâmetro, vai trazer todos os produtos da base de dados
        /// Se colocado o parâmetro query, ele servirá como um filtro, trazendo todas as ocorrências de banco em que esse filtro apareça        
        /// Se colocado o parâmetro page, ele trará a pagina correspondente a consulta, no caso de haver mais de uma página
        /// </returns>        
        [HttpGet("getAllProducts")]        
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProdutosQuery getAllProductsQuery)
        {
            return Ok(await _mediator.Send(getAllProductsQuery));            
        }
                
        [HttpGet("getProductById")]        
        public async Task<IActionResult> GetProdutoByIdAsync([FromQuery] GetProdutoByIdQuery query)
        {
            try
            {
                return Ok(await _mediator.Send(query));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
        [HttpPost("registerProduct")]
        public async Task<IActionResult> CadastrarProduto(CreateProdutoCommand request)
        {
            await _mediator.Send(request);
            return Ok("Produto Cadastrado com sucesso!");
        }
                
        [HttpPut("EditProduct")]
        public async Task<IActionResult> EditarProduto(UpdateProdutoCommand request)
        {
            try
            {
                await _mediator.Send(request);
                return Ok("Produto editado com sucesso");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeletarProduto([FromQuery] DeleteProdutoCommand request)
        {
            try
            {
                await _mediator.Send(request);                
                return Ok("Produto deletado da base de dados");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);                
            }            
        }
    }
}
