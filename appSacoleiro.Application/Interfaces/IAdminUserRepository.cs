
using appSacoleiro.Domain.Entities;

namespace appSacoleiro.Application.Interfaces;

public interface IAdminUserRepository
{
    Task<AdminUser?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task AdicionarAsync(
        AdminUser admin,
        CancellationToken cancellationToken = default);
}
