
using MediatR;

namespace appSacoleiro.Application.Pedidos.Commands.AtualizarStatusPedido;

public sealed record AtualizarStatusPedidoCommand(
    Guid PedidoId,
    string Status
) : IRequest<bool>;
