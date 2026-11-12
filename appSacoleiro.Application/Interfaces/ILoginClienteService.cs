
using appSacoleiro.Application.Autenticacao;

namespace appSacoleiro.Application.Interfaces;

public interface ILoginClienteService
{
    Task<LoginClienteResult?> LoginAsync(
        LoginClienteRequest request,
        CancellationToken cancellationToken = default);
}
