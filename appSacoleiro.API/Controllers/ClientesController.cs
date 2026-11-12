using appSacoleiro.Application.Clientes.Commands;
using appSacoleiro.Application.Clientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace appSacoleiro.API.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly ISender _sender;

    public ClientesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Criar(
        [FromBody] CriarClienteCommand command,
        CancellationToken cancellationToken)
    {
        var clienteId = await _sender.Send(
            command,
            cancellationToken);

        return Created(
            $"/api/clientes/{clienteId}",
            new { id = clienteId });
    }


    [HttpGet("{clienteId:guid}")]
    [Authorize(Roles = "Cliente,Admin")]
    public async Task<IActionResult> ObterPorId(
        Guid clienteId,
        CancellationToken cancellationToken)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var perfil = User.FindFirstValue(ClaimTypes.Role);

        if (!Guid.TryParse(id, out var usuarioId))
            return Unauthorized();

        if (perfil == "Cliente" && usuarioId != clienteId)
            return Forbid();

        var resultado = await _sender.Send(
            new ObterClientePorIdQuery(clienteId),
            cancellationToken);

        if (resultado is null)
            return NotFound(new { mensagem = "Cliente não encontrado." });

        return Ok(resultado);
    }

    [HttpPut("perfil")]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> AtualizarPerfil(
    [FromBody] AtualizarPerfilClienteRequest request,
    CancellationToken cancellationToken)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out var clienteId))
            return Unauthorized();

        var command = new AtualizarPerfilClienteCommand(
            clienteId,
            request.Nome,
            request.Telefone,
            request.Email);

        bool atualizado;

        try
        {
            atualizado = await _sender.Send(command, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }

        if (!atualizado)
            return NotFound(new { mensagem = "Cliente não encontrado." });

        return Ok(new { mensagem = "Perfil atualizado com sucesso." });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListarTodos(
    CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarClientesQuery(),
            cancellationToken);

        return Ok(resultado);
    }
}

public sealed record AtualizarPerfilClienteRequest(
    string Nome,
    string Telefone,
    string? Email);