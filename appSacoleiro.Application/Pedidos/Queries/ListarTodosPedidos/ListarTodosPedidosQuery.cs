
using appSacoleiro.Application.Pedidos.DTOs;
using MediatR;

namespace appSacoleiro.Application.Pedidos.Queries.ListarTodosPedidos;

public sealed record ListarTodosPedidosQuery()
    : IRequest<IReadOnlyList<PedidoDto>>;
