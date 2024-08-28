﻿using Microsoft.AspNetCore.Mvc;
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


        /// <summary>
        /// Lista todos os produtos. Pode usar um parâmetro adicional, de acordo com as instruções
        /// </summary>
        /// <param name="query"></param>
        /// <remarks>
        /// https://localhost:7277/api/Produto
        /// https://localhost:7277/api/Produto?query=****        
        /// </remarks>
        /// <returns>
        /// O parâmetro query não é obrigatório
        /// Se usado sem o parâmetro, vai trazer todos os produtos da base de dados
        /// Se colocado, ele servirá como um filtro, trazendo todas as ocorrências de banco em que esse filtro apareça        
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync(string? query)
        {         
            return Ok(await _produtoService.ListarProdutosAsync(query));
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
        public async Task<IActionResult> EditarProduto(int id, [FromBody] Produto request)
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
        public async Task<IActionResult> DeletarProduto(int id)
        {
            try
            {
                await _produtoService.DeletarProdutoAsync(id);
                return Ok("Produto deletado da base de dados");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);                
            }            
        }
    }
}
