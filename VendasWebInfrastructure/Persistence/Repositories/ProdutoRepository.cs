using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Repositories;
using VendasWebCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Azure.Core;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly VendasWebDbContext _dbContext;
        private const int PAGE_SIZE = 5;

        public ProdutoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;            
        }

        public async Task<PaginationResult<Produto>> ListarProdutos(string query, int page)
        {
            IQueryable<Produto> produtos = _dbContext.Produtos;
            if (!string.IsNullOrWhiteSpace(query))
            {
                produtos = produtos
                    .Where(p =>
                    p.NomeProduto.Contains(query));
            }

            return await produtos.GetPaged<Produto>(page, PAGE_SIZE);
        }

        public async Task<Produto> ListarProdutoEspecífico(int id)
        {
            try
            {
                var produto = await _dbContext.Produtos
                    .SingleOrDefaultAsync(p => p.IdProduto == id);
                if (produto is not null)
                {
                    return produto;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Produto não existente no banco de dados", ex);
            }
        }

        public async Task CadastrarProdutoAsync(Produto produto)
        {            
            await _dbContext.Produtos.AddAsync(produto);
            await SaveChangesASync();
        }

        public async Task DeletarProduto(int identity)
        {
            try
            {
                var produtos = _dbContext.Produtos.Attach(new Produto { IdProduto = identity });
                produtos.State = EntityState.Deleted;
                await SaveChangesASync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException("Produto não existente no banco de dados", e);
            }
        }

        public async Task EditarProdutoAsync(int id, Produto produto)
        {
            try
            {
                var produtoFinal = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == id);
                if (produtoFinal is not null)
                {
                    produtoFinal.Update(produto);
                    await SaveChangesASync();
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Produto não existente no banco de dados - Não atualizado", e);
            }
        }
        
        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
