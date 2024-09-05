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
                .HasKey(p => p.IdProduto);

            builder.Property(p => p.NomeProduto).HasMaxLength(20);
            builder.Property(p => p.Valor).HasPrecision(10, 2);

            builder
               .HasOne(p => p.User)
               .WithMany(p => p.Produtos)
               .HasForeignKey(p => p.UserId);
        }
    }
}
