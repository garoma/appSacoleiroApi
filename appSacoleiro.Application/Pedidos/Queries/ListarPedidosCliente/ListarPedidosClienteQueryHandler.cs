
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Application.Pedidos.DTOs;
using MediatR;

namespace appSacoleiro.Application.Pedidos.Queries.ListarPedidosCliente;

public sealed class ListarPedidosClienteQueryHandler(
    IPedidoRepository pedidoRepository)
    : IRequestHandler<ListarPedidosClienteQuery, IReadOnlyList<PedidoDto>>
{
    public async Task<IReadOnlyList<PedidoDto>> Handle(
        ListarPedidosClienteQuery request,
        CancellationToken cancellationToken)
    {
        var pedidos = await pedidoRepository.ListarPorClienteAsync(
            request.ClienteId,
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
