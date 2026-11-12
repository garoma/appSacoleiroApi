
using appSacoleiro.Application.Produtos.CriarProduto;
using appSacoleiro.Application.Produtos.ListarProdutos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using appSacoleiro.API.Models;

namespace appSacoleiro.API.Controllers;

[ApiController]
[Route("api/produtos")]
public sealed class ProdutosController(ISender sender) : ControllerBase
{

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] string? categoria,
        CancellationToken cancellationToken)
    {
        var produtos = await sender.Send(
            new ListarProdutosQuery(categoria),
            cancellationToken);

        return Ok(produtos);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Criar(
    [FromForm] CriarProdutoForm form,
    CancellationToken cancellationToken)
    {
        var (imagem1, imagem1ContentType, erroImagem1) =
            await LerArquivoAsync(
                form.Imagem1,
                2 * 1024 * 1024,
                ["image/jpeg", "image/png", "image/webp"],
                "A primeira imagem",
                cancellationToken);

        if (erroImagem1 is not null)
            return BadRequest(new { mensagem = erroImagem1 });

        var (imagem2, imagem2ContentType, erroImagem2) =
            await LerArquivoAsync(
                form.Imagem2,
                2 * 1024 * 1024,
                ["image/jpeg", "image/png", "image/webp"],
                "A segunda imagem",
                cancellationToken);

        if (erroImagem2 is not null)
            return BadRequest(new { mensagem = erroImagem2 });

        var (video, videoContentType, erroVideo) =
            await LerArquivoAsync(
                form.Video,
                20 * 1024 * 1024,
                ["video/mp4"],
                "O vídeo",
                cancellationToken);

        if (erroVideo is not null)
            return BadRequest(new { mensagem = erroVideo });

        var command = new CriarProdutoCommand(
            form.Nome,
            form.Descricao ?? string.Empty,
            form.Preco,
            form.Categoria,
            imagem1,
            imagem1ContentType,
            imagem2,
            imagem2ContentType,
            video,
            videoContentType);

        var id = await sender.Send(command, cancellationToken);

        return Created(
            $"/api/produtos/{id}",
            new
            {
                id,
                mensagem = "Produto cadastrado com sucesso."
            });
    }

    private static async Task<(
        byte[]? Arquivo,
        string? ContentType,
        string? Erro)> LerArquivoAsync(
            IFormFile? arquivo,
            long tamanhoMaximo,
            string[] formatosPermitidos,
            string nomeArquivo,
            CancellationToken cancellationToken)
    {
        if (arquivo is null)
            return (null, null, null);

        if (arquivo.Length == 0)
            return (null, null, $"{nomeArquivo}: o arquivo está vazio.");

        if (arquivo.Length > tamanhoMaximo)
        {
            var limiteMb = tamanhoMaximo / (1024 * 1024);

            return (
                null,
                null,
                $"{nomeArquivo}: o tamanho máximo permitido é {limiteMb} MB.");
        }

        if (!formatosPermitidos.Contains(
            arquivo.ContentType,
            StringComparer.OrdinalIgnoreCase))
        {
            return (
                null,
                null,
                $"{nomeArquivo}: formato inválido.");
        }

        using var memoryStream = new MemoryStream();

        await arquivo.CopyToAsync(
            memoryStream,
            cancellationToken);

        return (
            memoryStream.ToArray(),
            arquivo.ContentType,
            null);
    }
}
