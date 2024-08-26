using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebInfrastructure.Persistence.Repositories;

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

        
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {     
            var listaPedidos = await _pedidoService.ListarPedidosAsync();
            return Ok(listaPedidos);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoByIdAsync(int id)
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
        public async Task<IActionResult> Put(int id, [FromBody] Pedido request)
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {             
                await _pedidoService.DeletarPedidoAsync(id);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
