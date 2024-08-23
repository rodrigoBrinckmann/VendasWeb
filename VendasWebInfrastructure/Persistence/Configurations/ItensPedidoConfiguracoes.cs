using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendasWebCore.Entities;

namespace VendasWebInfrastructure.Persistence.Configurations
{
    public class ItensPedidoConfiguracoes : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder
                .HasKey(i => i.Identity);

            builder
               .HasOne(p => p.Pedido)
               .WithMany(p => p.ItensPedidos)
               .HasForeignKey(p => p.IdPedido);

            builder
                .HasOne(p => p.Produto)
                .WithMany(p => p.ItensPedidos)
                .HasForeignKey(p => p.IdProduto);
        }
    }
}
