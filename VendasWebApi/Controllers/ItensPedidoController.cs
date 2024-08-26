using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Services;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensPedidoController : ControllerBase
    {
        private readonly IItensPedidoService _itensPedidoService;

        public ItensPedidoController(IItensPedidoService itensPedidoService)
        {
            _itensPedidoService = itensPedidoService;
        }

        // GET: api/<PedidoController>
        [HttpGet]
        public async Task<IActionResult> GetAllItensPedidoAsync()
        {            
            return Ok(await _itensPedidoService.ListarItensPedidosAsync());
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItensPedidoByIdAsync(int id)
        {            
            return Ok(await _itensPedidoService.ListarItensPedidoAsync(id));
        }

        // POST api/<PedidoController>
        [HttpPost]
        public async Task<IActionResult> CadastrarItensPedido([FromBody] ItensPedido itensPedido)
        {
            await _itensPedidoService.CadastrarItensPedidoAsync(itensPedido);            
            return Ok("Itens do pedido cadastrado com sucesso!");
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ItensPedido request)
        {
            var itensPedido = await _itensPedidoService.EditarItensPedidoAsync(id, request);
            if (itensPedido.IdPedido != 0)
                return Ok();
            else
                return BadRequest("ItensPedido não encontrado na base de dados");
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _itensPedidoService.DeletarItensPedidoAsync(id);                
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}