using MediatR;

namespace appSacoleiro.Application.Produtos.CriarProduto;

public sealed record CriarProdutoCommand(
    string Nome,
    string Descricao,
    decimal Preco,
    string Categoria,
    byte[]? Imagem1 = null,
    string? Imagem1ContentType = null,
    byte[]? Imagem2 = null,
    string? Imagem2ContentType = null,
    byte[]? Video = null,
    string? VideoContentType = null
) : IRequest<Guid>;