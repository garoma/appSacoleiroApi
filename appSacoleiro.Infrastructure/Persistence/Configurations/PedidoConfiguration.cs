
using appSacoleiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appSacoleiro.Infrastructure.Persistence.Configurations;

public sealed class PedidoConfiguration
    : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PrecoUnitario)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.Quantidade)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.CriadoEmUtc)
            .IsRequired();

        builder.HasOne(p => p.Cliente)
            .WithMany()
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Produto)
            .WithMany()
            .HasForeignKey(p => p.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
