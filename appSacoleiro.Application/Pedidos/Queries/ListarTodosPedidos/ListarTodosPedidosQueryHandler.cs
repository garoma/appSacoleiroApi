
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Application.Pedidos.DTOs;
using MediatR;

namespace appSacoleiro.Application.Pedidos.Queries.ListarTodosPedidos;

public sealed class ListarTodosPedidosQueryHandler(
    IPedidoRepository pedidoRepository)
    : IRequestHandler<ListarTodosPedidosQuery, IReadOnlyList<PedidoDto>>
{
    public async Task<IReadOnlyList<PedidoDto>> Handle(
        ListarTodosPedidosQuery request,
        CancellationToken cancellationToken)
    {
        var pedidos = await pedidoRepository.ListarTodosAsync(
            cancellationToken);

        return pedidos.Select(p => new PedidoDto(
            p.Id,
            p.ClienteId,
            p.Cliente?.Nome ?? "Cliente",
            p.ProdutoId,
            p.Quantidade,
            p.PrecoUnitario,
            p.Total,
            p.Status,
            p.CriadoEmUtc
        )).ToList();
    }
}
