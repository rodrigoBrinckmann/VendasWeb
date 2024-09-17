using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasWebApplication.Commands.ItensPedidoCommands.CadastrarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.DeletarItensPedido;
using VendasWebApplication.Commands.ItensPedidoCommands.EditarItensPedido;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Queries.GetAllItensPedidos;
using VendasWebApplication.Queries.GetItemPedidoById;
using VendasWebApplication.Validators;
using VendasWebCore.Entities;

namespace VendasWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ItensPedidoController : ControllerBase
    {        
        private readonly IMediator _mediator;
        private readonly IValidator<DeletarItensPedidoCommand> _deletarItensPedidoCommandValidator;
        private readonly IValidator<EditarItensPedidoCommand> _editarItensPedidoCommandValidator;
        

        public ItensPedidoController(IMediator mediator, IValidator<DeletarItensPedidoCommand> deletarItensPedidoCommandValidator, IValidator<EditarItensPedidoCommand> editarItensPedidoCommandValidator)
        {
            _mediator = mediator;
            _deletarItensPedidoCommandValidator = deletarItensPedidoCommandValidator;
            _editarItensPedidoCommandValidator = editarItensPedidoCommandValidator;
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
            var inputValidator = await _editarItensPedidoCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }

            var itensPedido = await _mediator.Send(request);
            if (itensPedido.IdPedido != 0)
                return Ok("ItemPedido editado com sucesso");
            else
                return BadRequest("ItensPedido não encontrado na base de dados");
            
        }

        [HttpDelete("deleteProductOrder")]
        public async Task<IActionResult> DeletarItemPedido([FromQuery] DeletarItensPedidoCommand request)
        {
            var inputValidator = await _deletarItensPedidoCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }

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