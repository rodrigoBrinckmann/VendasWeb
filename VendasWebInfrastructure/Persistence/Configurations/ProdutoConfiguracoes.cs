using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendasWebCore.Entities;

namespace VendasWebInfrastructure.Persistence.Configurations
{
    public class ProdutoConfiguracoes : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            //Produto
            builder
                .HasKey(p => p.Identity);

            builder
                .HasMany(p => p.ItensPedidos)
                .WithOne()
                .HasForeignKey(p => p.Identity)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
