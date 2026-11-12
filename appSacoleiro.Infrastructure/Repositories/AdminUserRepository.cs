
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using appSacoleiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appSacoleiro.Infrastructure.Repositories;

public sealed class AdminUserRepository(
    appSacoleiroDbContext context) : IAdminUserRepository
{
    public Task<AdminUser?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim().ToLowerInvariant();

        return context.AdminUsers
            .SingleOrDefaultAsync(
                x => x.Email == emailNormalizado,
                cancellationToken);
    }

    public async Task AdicionarAsync(
        AdminUser admin,
        CancellationToken cancellationToken = default)
    {
        await context.AdminUsers.AddAsync(admin, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
