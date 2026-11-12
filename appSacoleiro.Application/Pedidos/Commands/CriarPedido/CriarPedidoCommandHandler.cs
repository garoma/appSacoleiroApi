
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using MediatR;

namespace appSacoleiro.Application.Pedidos.Commands.CriarPedido;

public sealed class CriarPedidoCommandHandler(
    IPedidoRepository pedidoRepository,
    IProdutoRepository produtoRepository)
    : IRequestHandler<CriarPedidoCommand, Guid>
{
    public async Task<Guid> Handle(
        CriarPedidoCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Quantidade <= 0)
            throw new ArgumentException(
                "A quantidade deve ser maior que zero.");

        var produto = await produtoRepository.ObterPorIdAsync(
            request.ProdutoId,
            cancellationToken);

        if (produto is null)
            throw new InvalidOperationException(
                "Produto não encontrado ou indisponível.");

        // O preço é obtido diretamente do banco de dados.
        var pedido = new Pedido(
            request.ClienteId,
            produto,
            request.Quantidade);

        await pedidoRepository.AdicionarAsync(
            pedido,
            cancellationToken);

        return pedido.Id;
    }
}
