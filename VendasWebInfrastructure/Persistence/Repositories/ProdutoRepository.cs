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

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly VendasWebDbContext _dbContext;        

        public ProdutoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;            
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

        public async Task<Produto> EditarProdutoAsync(int id, Produto produto)
        {
            try
            {
                var produtoFinal = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == id);
                if (produtoFinal is not null)
                {
                    produtoFinal.Update(produto);
                    await SaveChangesASync();
                    return produtoFinal;
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Produto não existente no banco de dados - Não atualizado", e);
            }
            return produto;
        }

        public async Task<Produto> ListarProdutoEspecífico(int identity)
        {            
            var produto = await _dbContext.Produtos.SingleOrDefaultAsync(p => p.IdProduto == identity);
            return produto;
        }

        public async Task<List<Produto>> ListarProdutos(string query)
        {
            IQueryable <Produto> produtos = _dbContext.Produtos;
            if (!string.IsNullOrWhiteSpace(query))
            {
                produtos = produtos
                    .Where(p =>
                    p.NomeProduto.Contains(query));
            }
            return await produtos.ToListAsync();
        }

        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
