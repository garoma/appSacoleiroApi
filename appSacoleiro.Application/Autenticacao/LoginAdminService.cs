
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace appSacoleiro.Application.Autenticacao;

public sealed class LoginAdminService(
    IAdminUserRepository adminRepository,
    IPasswordHasher<AdminUser> passwordHasher,
    ITokenService tokenService)
    : ILoginAdminService
{
    public async Task<LoginAdminResult?> LoginAsync(
        LoginAdminRequest request,
        CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var admin = await adminRepository.ObterPorEmailAsync(
            email,
            cancellationToken);

        if (admin is null)
            return null;

        var resultado = passwordHasher.VerifyHashedPassword(
            admin,
            admin.PasswordHash,
            request.Senha);

        if (resultado == PasswordVerificationResult.Failed)
            return null;

        var usuario = new UsuarioAutenticavel(
            admin.Id,
            admin.Nome,
            admin.Email,
            "Admin");

        var (token, expiraEmUtc) = tokenService.GerarToken(usuario);

        return new LoginAdminResult(
            token,
            expiraEmUtc,
            admin.Nome,
            admin.Email);
    }
}
