using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendasWebCore.Entities;

namespace VendasWebInfrastructure.Persistence.Configurations
{
    public class PedidoConfiguracoes : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder
            .HasKey(p => p.Identity);

            //Cardinalidade
            builder
                    .HasMany(p => p.ItensPedidos)
                    .WithOne()
                    .HasForeignKey(p => p.Identity)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
