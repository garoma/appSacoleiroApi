
using MediatR;

namespace appSacoleiro.Application.Clientes.Commands;

public sealed record CriarClienteCommand(
    string Nome,
    string Telefone,
    string? Email,
    string Senha) : IRequest<Guid>;
