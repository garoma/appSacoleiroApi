using MediatR;

namespace appSacoleiro.Application.Clientes.Commands;

public sealed record AtualizarPerfilClienteCommand(
    Guid ClienteId,
    string Nome,
    string Telefone,
    string? Email
) : IRequest<bool>;