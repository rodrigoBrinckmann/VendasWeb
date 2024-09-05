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

            builder
               .HasOne(p => p.Cliente)
               .WithMany(p => p.Pedidos)
               .HasForeignKey(p => p.UserId);
        }
    }
}
