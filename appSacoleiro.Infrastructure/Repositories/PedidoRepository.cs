
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using appSacoleiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appSacoleiro.Infrastructure.Repositories;

public sealed class PedidoRepository(
    appSacoleiroDbContext context) : IPedidoRepository
{
    public async Task AdicionarAsync(
        Pedido pedido,
        CancellationToken cancellationToken)
    {
        await context.Pedidos.AddAsync(pedido, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Pedido>> ListarPorClienteAsync(
        Guid clienteId,
        CancellationToken cancellationToken)
    {
        return await context.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Where(p => p.ClienteId == clienteId)
            .OrderByDescending(p => p.CriadoEmUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Pedido>> ListarTodosAsync(
        CancellationToken cancellationToken)
    {
        return await context.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .OrderByDescending(p => p.CriadoEmUtc)
            .ToListAsync(cancellationToken);
    }

    public Task<Pedido?> ObterPorIdAsync(
        Guid pedidoId,
        CancellationToken cancellationToken)
    {
        return context.Pedidos
            .FirstOrDefaultAsync(
                p => p.Id == pedidoId,
                cancellationToken);
    }

    public async Task AtualizarAsync(
        Pedido pedido,
        CancellationToken cancellationToken)
    {
        context.Pedidos.Update(pedido);
        await context.SaveChangesAsync(cancellationToken);
    }
}
