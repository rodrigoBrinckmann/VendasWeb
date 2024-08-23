using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VendasWebCore.Entities;

namespace VendasWebInfrastructure.Persistence
{
    public class VendasWebDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ItensPedido> ItensPedidos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite("DataSource=Vendas.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
