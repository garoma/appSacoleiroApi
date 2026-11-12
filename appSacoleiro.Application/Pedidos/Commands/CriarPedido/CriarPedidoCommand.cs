
using MediatR;

namespace appSacoleiro.Application.Pedidos.Commands.CriarPedido;

public sealed record CriarPedidoCommand(
    Guid ClienteId,
    Guid ProdutoId,
    int Quantidade
) : IRequest<Guid>;
