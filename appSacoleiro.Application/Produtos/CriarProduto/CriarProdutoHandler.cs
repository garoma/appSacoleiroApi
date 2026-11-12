
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using MediatR;

namespace appSacoleiro.Application.Produtos.CriarProduto;

public sealed class CriarProdutoHandler(
    IProdutoRepository produtoRepository)
    : IRequestHandler<CriarProdutoCommand, Guid>
{
    public async Task<Guid> Handle(
        CriarProdutoCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException(
                "O nome do produto é obrigatório.");

        if (request.Nome.Trim().Length > 150)
            throw new ArgumentException(
                "O nome deve ter no máximo 150 caracteres.");

        if (request.Preco <= 0)
            throw new ArgumentException(
                "O preço deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(request.Categoria))
            throw new ArgumentException(
                "A categoria é obrigatória.");

        if (request.Categoria.Trim().Length > 100)
            throw new ArgumentException(
                "A categoria deve ter no máximo 100 caracteres.");

        if (request.Descricao?.Length > 2000)
            throw new ArgumentException(
                "A descrição deve ter no máximo 2000 caracteres.");

        ValidarParMidia(
            request.Imagem1,
            request.Imagem1ContentType,
            "A primeira imagem");

        ValidarParMidia(
            request.Imagem2,
            request.Imagem2ContentType,
            "A segunda imagem");

        ValidarParMidia(
            request.Video,
            request.VideoContentType,
            "O vídeo");

        var nome = request.Nome.Trim();

        if (await produtoRepository.ExisteNomeAsync(
            nome, cancellationToken))
        {
            throw new InvalidOperationException(
                "Já existe um produto com esse nome.");
        }

        var produto = new Produto(
            nome,
            request.Descricao ?? string.Empty,
            request.Preco,
            request.Categoria.Trim());

        produto.AtualizarMidias(
            request.Imagem1,
            request.Imagem1ContentType,
            request.Imagem2,
            request.Imagem2ContentType,
            request.Video,
            request.VideoContentType);

        await produtoRepository.AdicionarAsync(
            produto, cancellationToken);

        return produto.Id;
    }

    private static void ValidarParMidia(
        byte[]? arquivo,
        string? contentType,
        string nomeMidia)
    {
        if (arquivo is null && contentType is null)
            return;

        if (arquivo is null || string.IsNullOrWhiteSpace(contentType))
        {
            throw new ArgumentException(
                $"{nomeMidia}: arquivo e formato devem ser informados juntos.");
        }
    }
}
