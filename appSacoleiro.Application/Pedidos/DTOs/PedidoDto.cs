
namespace appSacoleiro.Application.Pedidos.DTOs;

public sealed record PedidoDto(
    Guid Id,
    Guid ClienteId,
    string ClienteNome,
    Guid ProdutoId,
    int Quantidade,
    decimal PrecoUnitario,
    decimal Total,
    string Status,
    DateTime CriadoEmUtc
);
