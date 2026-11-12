
namespace appSacoleiro.Application.Autenticacao;

public sealed record LoginAdminResult(
    string Token,
    DateTime ExpiraEmUtc,
    string Nome,
    string Email);
