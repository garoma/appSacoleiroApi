
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace appSacoleiro.Application.Clientes.Commands;

public sealed class CriarClienteCommandHandler
    : IRequestHandler<CriarClienteCommand, Guid>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IPasswordHasher<Cliente> _passwordHasher;

    public CriarClienteCommandHandler(
        IClienteRepository clienteRepository,
        IPasswordHasher<Cliente> passwordHasher)
    {
        _clienteRepository = clienteRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        CriarClienteCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Senha) ||
            request.Senha.Length < 8)
        {
            throw new ArgumentException(
                "A senha deve possuir pelo menos 8 caracteres.");
        }

        var cliente = new Cliente(
            request.Nome,
            request.Telefone,
            request.Email);

        var senhaHash = _passwordHasher.HashPassword(
            cliente,
            request.Senha);

        cliente.DefinirSenhaHash(senhaHash);

        await _clienteRepository.AdicionarAsync(
            cliente,
            cancellationToken);

        await _clienteRepository.SalvarAlteracoesAsync(
            cancellationToken);

        return cliente.Id;
    }
}
