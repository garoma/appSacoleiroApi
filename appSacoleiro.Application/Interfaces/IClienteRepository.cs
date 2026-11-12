
using appSacoleiro.Domain.Entities;

namespace appSacoleiro.Application.Interfaces;

public interface IClienteRepository
{
    Task AdicionarAsync(
        Cliente cliente,
        CancellationToken cancellationToken = default);

    Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default);

    Task<Cliente?> ObterPorTelefoneAsync(
        string telefone,
        CancellationToken cancellationToken = default);

    Task<Cliente?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<Cliente?> ObterPorIdAsync(
       Guid id,
       CancellationToken cancellationToken = default);

    Task<bool> TelefoneExisteParaOutroClienteAsync(
    string telefone,
    Guid clienteId,
    CancellationToken cancellationToken = default);

    Task<bool> EmailExisteParaOutroClienteAsync(
        string email,
        Guid clienteId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Cliente>> ListarTodosAsync(
        CancellationToken cancellationToken = default);
}
