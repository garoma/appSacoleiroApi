namespace appSacoleiro.Application.Produtos.DTOs;

public sealed record ProdutoListagemDto(
    Guid Id,
    string Nome,
    string Descricao,
    decimal Preco,
    string Categoria,
    bool Ativo,
    DateTime CriadoEmUtc,
    byte[]? Imagem1,
    string? Imagem1ContentType,
    byte[]? Imagem2,
    string? Imagem2ContentType,
    byte[]? Video,
    string? VideoContentType
);