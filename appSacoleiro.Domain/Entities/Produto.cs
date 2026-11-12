
namespace appSacoleiro.Domain.Entities;

public sealed class Produto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public decimal Preco { get; private set; }
    public string Categoria { get; private set; } = string.Empty;

    public byte[]? Imagem1 { get; private set; }
    public string? Imagem1ContentType { get; private set; }

    public byte[]? Imagem2 { get; private set; }
    public string? Imagem2ContentType { get; private set; }

    public byte[]? Video { get; private set; }
    public string? VideoContentType { get; private set; }

    public bool Ativo { get; private set; }
    public DateTime CriadoEmUtc { get; private set; }

    private Produto() { }

    public Produto(
        string nome,
        string descricao,
        decimal preco,
        string categoria)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(categoria))
            throw new ArgumentException("A categoria é obrigatória.");

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        Preco = preco;
        Categoria = categoria.Trim();
        Ativo = true;
        CriadoEmUtc = DateTime.UtcNow;
    }

    public void Atualizar(
        string nome,
        string descricao,
        decimal preco,
        string categoria)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(categoria))
            throw new ArgumentException("A categoria é obrigatória.");

        Nome = nome.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        Preco = preco;
        Categoria = categoria.Trim();
    }

    public void AtualizarMidias(
        byte[]? imagem1,
        string? imagem1ContentType,
        byte[]? imagem2,
        string? imagem2ContentType,
        byte[]? video,
        string? videoContentType)
    {
        if (imagem1 is not null)
        {
            ValidarImagem(imagem1, imagem1ContentType);
            Imagem1 = imagem1;
            Imagem1ContentType = imagem1ContentType;
        }

        if (imagem2 is not null)
        {
            ValidarImagem(imagem2, imagem2ContentType);
            Imagem2 = imagem2;
            Imagem2ContentType = imagem2ContentType;
        }

        if (video is not null)
        {
            if (video.Length > 20 * 1024 * 1024)
                throw new ArgumentException("O vídeo não pode ultrapassar 20 MB.");

            if (videoContentType != "video/mp4")
                throw new ArgumentException("Formato inválido. Use MP4.");

            Video = video;
            VideoContentType = videoContentType;
        }
    }

    private static void ValidarImagem(byte[] imagem, string? contentType)
    {
        if (imagem.Length == 0)
            throw new ArgumentException("A imagem não pode estar vazia.");

        if (imagem.Length > 2 * 1024 * 1024)
            throw new ArgumentException("Cada imagem não pode ultrapassar 2 MB.");

        if (contentType is not ("image/jpeg" or "image/png" or "image/webp"))
            throw new ArgumentException("Formato inválido. Use JPEG, PNG ou WebP.");
    }

    public void Desativar() => Ativo = false;

    public void Ativar() => Ativo = true;
}
