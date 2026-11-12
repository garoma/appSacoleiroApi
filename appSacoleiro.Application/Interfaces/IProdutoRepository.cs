
using appSacoleiro.Application.Produtos.DTOs;
using appSacoleiro.Domain.Entities;

namespace appSacoleiro.Application.Interfaces;

public interface IProdutoRepository
{
    Task AdicionarAsync(
        Produto produto,
        CancellationToken cancellationToken);

    Task<bool> ExisteNomeAsync(
        string nome,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ProdutoListagemDto>> ListarAsync(
        string? categoria,
        CancellationToken cancellationToken);

    Task<Produto?> ObterPorIdAsync(
    Guid id,
    CancellationToken cancellationToken);
}
