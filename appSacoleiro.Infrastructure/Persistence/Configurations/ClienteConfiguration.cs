
using appSacoleiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appSacoleiro.Infrastructure.Persistence.Configurations;

public sealed class ClienteConfiguration
    : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.SenhaHash)
            .HasColumnName("senha_hash")
            .HasMaxLength(500)
            .IsRequired()
            .HasDefaultValue("");

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasMaxLength(200);

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.CriadoEmUtc)
            .HasColumnName("criado_em_utc")
            .IsRequired();

        builder.HasIndex(p => p.Telefone);
    }
}
