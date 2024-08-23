using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Repositories;
using VendasWebCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace VendasWebInfrastructure.Persistence.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly VendasWebDbContext _dbContext;
        //private readonly string _connectionString;

        public ProdutoRepository(VendasWebDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            //_connectionString = configuration.GetConnectionString("VendasWebDBCs");
        }

        public async Task CadastrarProdutoASync(Produto produto)
        {
            //// Instância da entidade
            var category = new Produto() { NomeProduto = "Teste1", Valor = 10.00m };            
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> DeletarProduto()
        {
            throw new NotImplementedException();
        }

        public Task EditarProdutoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Produto> ListarProdutoEspecífico(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            var produtos = await _dbContext.Produtos.ToListAsync();
            return produtos.ToList();
        }

        public async Task SaveChangesASync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
