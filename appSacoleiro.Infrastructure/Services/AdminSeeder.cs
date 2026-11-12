
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace appSacoleiro.Infrastructure.Services;

public sealed class AdminSeeder(
    IConfiguration configuration,
    IAdminUserRepository adminRepository,
    IPasswordHasher<AdminUser> passwordHasher)
{
    public async Task SeedAsync(
        CancellationToken cancellationToken = default)
    {
        var nome = configuration["Admin:Nome"];
        var email = configuration["Admin:Email"];
        var senha = configuration["Admin:Password"];

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha))
        {
            throw new InvalidOperationException(
                "Configure Admin:Nome, Admin:Email e Admin:Password " +
                "nos segredos locais antes de iniciar a API.");
        }

        if (senha.Length < 12)
        {
            throw new InvalidOperationException(
                "A senha inicial do administrador deve ter " +
                "pelo menos 12 caracteres.");
        }

        var existente = await adminRepository.ObterPorEmailAsync(
            email,
            cancellationToken);

        if (existente is not null)
            return;

        var admin = new AdminUser(nome, email, "temporario");

        var hash = passwordHasher.HashPassword(admin, senha);
        admin.AtualizarPasswordHash(hash);

        await adminRepository.AdicionarAsync(
            admin,
            cancellationToken);
    }
}
