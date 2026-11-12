
using appSacoleiro.Domain.Entities;

namespace appSacoleiro.Application.Interfaces;

public interface IPedidoRepository
{
    Task AdicionarAsync(
        Pedido pedido,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Pedido>> ListarPorClienteAsync(
        Guid clienteId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Pedido>> ListarTodosAsync(
        CancellationToken cancellationToken);

    Task<Pedido?> ObterPorIdAsync(
        Guid pedidoId,
        CancellationToken cancellationToken);

    Task AtualizarAsync(
        Pedido pedido,
        CancellationToken cancellationToken);
}
