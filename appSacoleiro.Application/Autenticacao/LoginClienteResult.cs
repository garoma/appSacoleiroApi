
namespace appSacoleiro.Application.Autenticacao;

public sealed record LoginClienteResult(
    Guid ClienteId,
    string Token,
    DateTime ExpiraEmUtc,
    string Nome,
    string Telefone,
    string Email);
