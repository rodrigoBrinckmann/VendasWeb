using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;
using VendasWebApplication.Commands.PedidoCommands.EditarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;
using VendasWebApplication.Queries.GetAllPedidos;
using VendasWebApplication.Queries.GetPedidoById;
using VendasWebCore.Entities;
using VendasWebCore.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VendasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {           
        private readonly IMediator _mediator;

        public PedidoController(IMediator mediator)
        {            
            _mediator = mediator;
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
        public async Task<IActionResult> GetAllOrdersAsync([FromQuery]GetAllPedidosQuery query)
        {            
            var listaPedidos = await _mediator.Send(query);
            return Ok(listaPedidos);
        }

        
        [HttpGet]
        [Route("getPedidoEspecífico")]
        public async Task<IActionResult> GetOrderByIdAsync([FromQuery] GetPedidoByIdQuery query)
        {
            var pedido = await _mediator.Send(query);            
            return Ok(pedido);
        }

        
        [HttpPost]
        public async Task<IActionResult> CadastrarPedido([FromBody] CriarPedidoCommand pedido)
        {
            await _mediator.Send(pedido);            
            return Ok("Pedido Cadastrado com sucesso!");
        }

        
        [HttpPut]
        public async Task<IActionResult> EditarPedido(EditarPedidoCommand editCommand)
        {
            try
            {                
                var pedidofinal = await _mediator.Send(editCommand);
                return Ok(pedidofinal);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("registraPagamento")]
        public async Task<IActionResult> RegistrarPagamento(RegistraPagamentoCommand pagamentoCommand)
        {
            try
            {
                var pedidofinal = await _mediator.Send(pagamentoCommand);
                return Ok(pedidofinal);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPedido(DeletarPedidoCommand deleteCommand)
        {
            try
            {
                await _mediator.Send(deleteCommand);
                return Ok("Pedido deletado com sucesso!");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
