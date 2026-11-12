
namespace appSacoleiro.Application.Interfaces;

public sealed record UsuarioAutenticavel(
    Guid Id,
    string Nome,
    string Email,
    string Perfil);
