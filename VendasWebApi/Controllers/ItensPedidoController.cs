using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.ItensPedidoCommands.CadastrarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido;
using VendasWebApplication.Queries.GetAllItensPedidos;
using VendasWebApplication.Queries.GetItemPedidoById;

namespace VendasWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ItensPedidoController : ControllerBase
    {        
        private readonly IMediator _mediator;

        public ItensPedidoController(IMediator mediator)
        {
            _mediator = mediator;
        }
                
        [HttpGet("getProductOrder")]
        public async Task<IActionResult> GetAllItensPedidoAsync([FromQuery] GetAllItensPedidosQuery getAllItensPedidosQuery)
        {
            return Ok(await _mediator.Send(getAllItensPedidosQuery));                
        }

        
        [HttpGet("getProductOrderById")]        
        public async Task<IActionResult> GetItensPedidoByIdAsync([FromQuery] GetItemPedidoByIdQuery getItemPedidoByIdQuery)
        {
            try
            {
                return Ok(await _mediator.Send(getItemPedidoByIdQuery));                
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }                
        }

        
        [HttpPost("registerProductOrder")]
        public async Task<IActionResult> CadastrarItensPedido([FromBody] CadastrarItensPedidoCommand itensPedidoList)
        {
            await _mediator.Send(itensPedidoList);            
            return Ok("Itens do pedido cadastrado com sucesso!");
        }

        
        [HttpPut("editProductOrder")]
        public async Task<IActionResult> EditarItemPedido([FromBody] EditarItensPedidoCommand request)
        {            
            var itensPedido = await _mediator.Send(request);
            if (itensPedido.IdPedido != 0)
                return Ok("ItemPedido editado com sucesso");
            else
                return BadRequest("ItensPedido não encontrado na base de dados");
            
        }

        [HttpDelete("deleteProductOrder")]
        public async Task<IActionResult> DeletarItemPedido([FromQuery] DeletarItensPedidoCommand request)
        {
            try
            {
                await _mediator.Send(request);                        
                return Ok("ItemPedido deletado com sucesso");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}