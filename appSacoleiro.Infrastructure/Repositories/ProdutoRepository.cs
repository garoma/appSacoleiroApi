
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Application.Produtos.DTOs;
using appSacoleiro.Domain.Entities;
using appSacoleiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appSacoleiro.Infrastructure.Repositories;

public sealed class ProdutoRepository(
    appSacoleiroDbContext context)
    : IProdutoRepository
{
    public async Task AdicionarAsync(
        Produto produto,
        CancellationToken cancellationToken)
    {
        await context.Produtos.AddAsync(
            produto, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExisteNomeAsync(
        string nome,
        CancellationToken cancellationToken)
    {
        return context.Produtos.AnyAsync(
            p => p.Nome.ToLower() == nome.ToLower(),
            cancellationToken);
    }

    public async Task<IReadOnlyList<ProdutoListagemDto>> ListarAsync(
        string? categoria,
        CancellationToken cancellationToken)
    {
        var consulta = context.Produtos
            .AsNoTracking()
            .Where(p => p.Ativo);

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            var categoriaNormalizada = categoria.Trim();

            consulta = consulta.Where(
                p => p.Categoria.ToLower()
                    == categoriaNormalizada.ToLower());
        }

        return await consulta
            .OrderBy(p => p.Nome)
            .Select(p => new ProdutoListagemDto(
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco,
                p.Categoria,
                p.Ativo,
                p.CriadoEmUtc,
                p.Imagem1,
                p.Imagem1ContentType,
                p.Imagem2,
                p.Imagem2ContentType,
                p.Video,
                p.VideoContentType))
            .ToListAsync(cancellationToken);
    }

    public Task<Produto?> ObterPorIdAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        return context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                p => p.Id == id && p.Ativo,
                cancellationToken);
    }
}
