
using appSacoleiro.Application.Pedidos.DTOs;
using MediatR;

namespace appSacoleiro.Application.Pedidos.Queries.ListarPedidosCliente;

public sealed record ListarPedidosClienteQuery(
    Guid ClienteId
) : IRequest<IReadOnlyList<PedidoDto>>;
