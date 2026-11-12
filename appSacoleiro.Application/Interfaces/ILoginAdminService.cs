
using appSacoleiro.Application.Autenticacao;

namespace appSacoleiro.Application.Interfaces;

public interface ILoginAdminService
{
    Task<LoginAdminResult?> LoginAsync(
        LoginAdminRequest request,
        CancellationToken cancellationToken = default);
}
