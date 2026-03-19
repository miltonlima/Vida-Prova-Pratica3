using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class ItemConfiguration : IEntityTypeConfiguration<SolicitacaoAgg.Item>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgg.Item> builder)
        {
            builder.ToTable("Item");
            builder.Ignore(c => c.Subtotal);
            builder.HasOne(c => c.Produto).WithMany();
        }
    }
}
