
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace appSacoleiro.Application.Autenticacao;

public sealed class LoginClienteService(
    IClienteRepository clienteRepository,
    IPasswordHasher<Cliente> passwordHasher,
    ITokenService tokenService)
    : ILoginClienteService
{
    public async Task<LoginClienteResult?> LoginAsync(
        LoginClienteRequest request,
        CancellationToken cancellationToken = default)
    {

        string telefone = "";
        string email = "";

        if (request.Identificador.Contains('@'))
        {
            email = request.Identificador.Trim();
        }
        else
        {
            telefone = request.Identificador.Trim();

        }

        var cliente = !string.IsNullOrWhiteSpace(email)
            ? await clienteRepository.ObterPorEmailAsync(
                email,
                cancellationToken)
            : await clienteRepository.ObterPorTelefoneAsync(
                telefone,
                cancellationToken);

        if (cliente is null ||
            string.IsNullOrWhiteSpace(cliente.SenhaHash) ||
            cliente.Status != "Ativo")
        {
            return null;
        }

        var resultado = passwordHasher.VerifyHashedPassword(
            cliente,
            cliente.SenhaHash,
            request.Senha);

        if (resultado == PasswordVerificationResult.Failed)
            return null;

        var usuario = new UsuarioAutenticavel(
            cliente.Id,
            cliente.Nome,
            cliente.Email ?? string.Empty,
            "Cliente");

        var (token, expiraEmUtc) = tokenService.GerarToken(usuario);

        return new LoginClienteResult(
            cliente.Id,
            token,
            expiraEmUtc,
            cliente.Nome,
            cliente.Telefone ?? string.Empty,
            cliente.Email ?? string.Empty);
    }
}
