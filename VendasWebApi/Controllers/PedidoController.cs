using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Commands.PedidoCommands.DeletarPedido;
using VendasWebApplication.Commands.PedidoCommands.RegistraPagamento;
using VendasWebApplication.Queries.GetAllPedidos;
using VendasWebApplication.Queries.GetPedidoById;

namespace VendasWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PedidoController : ControllerBase
    {           
        private readonly IMediator _mediator;
        private readonly IValidator<CriarPedidoCommand> _criarPedidoValidator;
        private readonly IValidator<DeletarPedidoCommand> _deletarPedidoValidator;
        private readonly IValidator<RegistraPagamentoCommand> _registraPagamentoValidator;

        public PedidoController(IMediator mediator, IValidator<CriarPedidoCommand> criarPedidoValidator, IValidator<DeletarPedidoCommand> deletarPedidoValidator, IValidator<RegistraPagamentoCommand> registraPagamentoValidator)
        {            
            _mediator = mediator;
            _criarPedidoValidator = criarPedidoValidator;
            _deletarPedidoValidator = deletarPedidoValidator;
            _registraPagamentoValidator = registraPagamentoValidator;
        }



        /// <summary>
        /// Lista todos os pedidos. Pode usar um parâmetro adicional, de acordo com as instruções
        /// </summary>
        /// <param name="getAllPedidosQuery"></param>
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
        [HttpGet("getAllOrders")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllOrdersAsync([FromQuery] GetAllPedidosQuery getAllPedidosQuery)
        {            
            var listaPedidos = await _mediator.Send(getAllPedidosQuery);
            return Ok(listaPedidos);
        }

        
        [HttpGet("getOrderById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderByIdAsync([FromQuery] GetPedidoByIdQuery query)
        {
            var pedido = await _mediator.Send(query);            
            if (pedido is not null)
                return Ok(pedido);
            else return BadRequest("Pedido não cadastrado na base de dados");
        }

        
        [HttpPost("registerOrder")]
        [Authorize(Roles = "Admin, Sales")]
        public async Task<IActionResult> CadastrarPedido([FromBody] CriarPedidoCommand pedido)
        {
            var inputValidator = await _criarPedidoValidator.ValidateAsync(pedido, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }

            var id = await _mediator.Send(pedido);            
            return Ok($"Pedido {id} Cadastrado com sucesso!");
        }                

        [HttpPut("registerPayment")]
        [Authorize(Roles = "Admin, Sales")]
        public async Task<IActionResult> RegistrarPagamento(RegistraPagamentoCommand pagamentoCommand)
        {
            var inputValidator = await _registraPagamentoValidator.ValidateAsync(pagamentoCommand, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }

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

        [HttpDelete("deleteOrder")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletarPedido([FromQuery] DeletarPedidoCommand deleteCommand)
        {
            var inputValidator = await _deletarPedidoValidator.ValidateAsync(deleteCommand, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }
            try
            {
                await _mediator.Send(deleteCommand);
                return Ok("Pedido deletado com sucesso!");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
