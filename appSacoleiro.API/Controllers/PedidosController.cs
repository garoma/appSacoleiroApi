
using System.Security.Claims;
using appSacoleiro.Application.Pedidos.Commands.AtualizarStatusPedido;
using appSacoleiro.Application.Pedidos.Commands.CriarPedido;
using appSacoleiro.Application.Pedidos.Queries.ListarPedidosCliente;
using appSacoleiro.Application.Pedidos.Queries.ListarTodosPedidos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appSacoleiro.API.Controllers;

[ApiController]
[Route("api/pedidos")]
public sealed class PedidosController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> Criar(
        [FromBody] CriarPedidoRequest request,
        CancellationToken cancellationToken)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out var clienteId))
            return Unauthorized();

        var pedidoId = await sender.Send(
            new CriarPedidoCommand(
                clienteId,
                request.ProdutoId,
                request.Quantidade),
            cancellationToken);

        return Ok(new
        {
            pedidoId,
            mensagem = "Pedido criado com sucesso."
        });
    }

    [HttpGet("meus")]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> ListarMeusPedidos(
        CancellationToken cancellationToken)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out var clienteId))
            return Unauthorized();

        var pedidos = await sender.Send(
            new ListarPedidosClienteQuery(clienteId),
            cancellationToken);

        return Ok(pedidos);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListarTodos(
        CancellationToken cancellationToken)
    {
        var pedidos = await sender.Send(
            new ListarTodosPedidosQuery(),
            cancellationToken);

        return Ok(pedidos);
    }

    [HttpPatch("{pedidoId:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AtualizarStatus(
        Guid pedidoId,
        [FromBody] AtualizarStatusPedidoRequest request,
        CancellationToken cancellationToken)
    {
        var atualizado = await sender.Send(
            new AtualizarStatusPedidoCommand(
                pedidoId,
                request.Status),
            cancellationToken);

        return Ok(new
        {
            atualizado,
            mensagem = "Status do pedido atualizado com sucesso."
        });
    }
}

public sealed record CriarPedidoRequest(
    Guid ProdutoId,
    int Quantidade);

public sealed record AtualizarStatusPedidoRequest(
    string Status);
