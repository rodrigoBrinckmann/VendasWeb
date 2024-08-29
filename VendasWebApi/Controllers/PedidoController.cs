using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebCore.Entities;
using VendasWebCore.Services;

namespace VendasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {        
        private readonly IPedidoService _pedidoService; 

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }



        /// <summary>
        /// Lista todos os pedidos. Pode usar um parâmetro adicional, de acordo com as instruções
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <remarks>
        /// https://localhost:7277/api/Pedido
        ///
        /// https://localhost:7277/api/Pedido?query=****
        /// </remarks>
        /// <returns>
        /// O parâmetro query não é obrigatório
        /// Se usado sem o parâmetro, vai trazer todos os pedidos da base de dados
        /// Se colocado, ele servirá como um filtro, trazendo todas as ocorrências de banco em que esse filtro
        /// apareça ou no nome do cliente ou no email do cliente
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync(string? query, int page = 1)
        {     
            var listaPedidos = await _pedidoService.ListarPedidosAsync(query, page);
            return Ok(listaPedidos);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {         
            var pedido = await _pedidoService.ListarPedidoAsync(id);
            return Ok(pedido);
        }

        
        [HttpPost]
        public async Task<IActionResult> CadastrarPedido([FromBody] Pedido pedido)
        {
            await _pedidoService.CadastrarPedidoAsync(pedido);         
            return Ok("Pedido Cadastrado com sucesso!");
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarPedido(int id, [FromBody] Pedido request)
        {
            try
            {                
                var pedidofinal = await _pedidoService.EditarPedidoAsync(id, request);
                return Ok(pedidofinal);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPedido(int id)
        {
            try
            {             
                await _pedidoService.DeletarPedidoAsync(id);
                return Ok("Pedido deletado com sucesso!");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
