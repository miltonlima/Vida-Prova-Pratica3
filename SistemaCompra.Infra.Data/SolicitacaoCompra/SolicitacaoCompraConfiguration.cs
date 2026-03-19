using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");

            builder.OwnsOne(c => c.TotalGeral, b => b.Property(c => c.Value).HasColumnName("TotalGeral"));
            builder.OwnsOne(c => c.UsuarioSolicitante, b => b.Property(c => c.Nome).HasColumnName("UsuarioSolicitante"));
            builder.OwnsOne(c => c.NomeFornecedor, b => b.Property(c => c.Nome).HasColumnName("NomeFornecedor"));

            builder.HasMany(c => c.Itens).WithOne();
        }
    }
}
