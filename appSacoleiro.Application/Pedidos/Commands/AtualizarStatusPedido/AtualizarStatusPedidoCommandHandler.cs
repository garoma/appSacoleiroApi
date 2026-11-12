
using appSacoleiro.Application.Interfaces;
using MediatR;

namespace appSacoleiro.Application.Pedidos.Commands.AtualizarStatusPedido;

public sealed class AtualizarStatusPedidoCommandHandler(
    IPedidoRepository pedidoRepository)
    : IRequestHandler<AtualizarStatusPedidoCommand, bool>
{
    public async Task<bool> Handle(
        AtualizarStatusPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorIdAsync(
            request.PedidoId,
            cancellationToken);

        if (pedido is null)
            throw new InvalidOperationException(
                "Pedido não encontrado.");

        pedido.AlterarStatus(request.Status);

        await pedidoRepository.AtualizarAsync(
            pedido,
            cancellationToken);

        return true;
    }
}
