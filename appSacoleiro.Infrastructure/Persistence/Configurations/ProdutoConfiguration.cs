
using appSacoleiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appSacoleiro.Infrastructure.Persistence.Configurations;

public sealed class ProdutoConfiguration
    : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(p => p.Preco)
            .HasColumnName("preco")
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(p => p.Categoria)
            .HasColumnName("categoria")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.Property(p => p.Imagem1)
            .HasColumnName("imagem1")
            .HasColumnType("bytea")
            .IsRequired(false);

        builder.Property(p => p.Imagem1ContentType)
            .HasColumnName("imagem1_content_type")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.Imagem2)
            .HasColumnName("imagem2")
            .HasColumnType("bytea")
            .IsRequired(false);

        builder.Property(p => p.Imagem2ContentType)
            .HasColumnName("imagem2_content_type")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.Video)
            .HasColumnName("video")
            .HasColumnType("bytea")
            .IsRequired(false);

        builder.Property(p => p.VideoContentType)
            .HasColumnName("video_content_type")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.CriadoEmUtc)
            .HasColumnName("criado_em_utc")
            .IsRequired();

        builder.HasIndex(p => p.Nome);
        builder.HasIndex(p => p.Categoria);
    }
}
