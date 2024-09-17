using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using VendasWebApplication.Commands.ProdutoCommands.CriarProduto;
using VendasWebApplication.Commands.ProdutoCommands.DeletarProduto;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebApplication.Validators;
using VendasWebCore.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendasWebApi.Controllers
{
    [Route("api/")]
    [ApiController]    
    public class ProdutoController : ControllerBase
    {        
        private readonly IMediator _mediator;
        private readonly IValidator<CreateProdutoCommand> _createProductvalidator;
        private readonly IValidator<DeleteProdutoCommand> _deleteProductvalidator;
        private readonly IValidator<UpdateProdutoCommand> _updateProductvalidator;

        public ProdutoController(IMediator mediator, IValidator<CreateProdutoCommand> createProductvalidator, IValidator<DeleteProdutoCommand> deleteProductvalidator, IValidator<UpdateProdutoCommand> updateProductvalidator)
        {            
            _mediator = mediator;
            _createProductvalidator = createProductvalidator;
            _deleteProductvalidator = deleteProductvalidator;
            _updateProductvalidator = updateProductvalidator;
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
        [Authorize(Roles = "Admin, Sales")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProdutosQuery getAllProductsQuery)
        {
            return Ok(await _mediator.Send(getAllProductsQuery));            
        }
                
        [HttpGet("getProductById")]
        [Authorize(Roles = "Admin, Sales")]
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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CadastrarProduto(CreateProdutoCommand request)
        {
            var productValidator = await _createProductvalidator.ValidateAsync(request, new CancellationToken());
            if (!productValidator.IsValid)
            {
                var errors = productValidator.ToDictionary();                

                return new BadRequestObjectResult(errors);
            }

            await _mediator.Send(request);
            return Ok("Produto Cadastrado com sucesso!");
        }
                
        [HttpPut("EditProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditarProduto(UpdateProdutoCommand request)
        {
            var productValidator = await _updateProductvalidator.ValidateAsync(request, new CancellationToken());
            if (!productValidator.IsValid)
            {
                var errors = productValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletarProduto([FromQuery] DeleteProdutoCommand request)
        {
            var productValidator = await _deleteProductvalidator.ValidateAsync(request, new CancellationToken());
            if (!productValidator.IsValid)
            {
                var errors = productValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }
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
