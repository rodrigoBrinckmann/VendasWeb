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
            .HasKey(p => p.IdPedido);

            builder.Property(p => p.NomeCliente).HasMaxLength(60);
            builder.Property(p => p.EmailCLiente).HasMaxLength(50);

        }
    }
}
