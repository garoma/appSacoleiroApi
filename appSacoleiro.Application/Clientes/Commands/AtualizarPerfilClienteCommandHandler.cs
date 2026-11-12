using appSacoleiro.Application.Interfaces;
using MediatR;

namespace appSacoleiro.Application.Clientes.Commands;

public sealed class AtualizarPerfilClienteCommandHandler(
    IClienteRepository clienteRepository)
    : IRequestHandler<AtualizarPerfilClienteCommand, bool>
{
    public async Task<bool> Handle(
        AtualizarPerfilClienteCommand request,
        CancellationToken cancellationToken)
    {
        var cliente = await clienteRepository.ObterPorIdAsync(
            request.ClienteId,
            cancellationToken);

        if (cliente is null)
            return false;

        var telefoneDuplicado =
            await clienteRepository.TelefoneExisteParaOutroClienteAsync(
                request.Telefone,
                request.ClienteId,
                cancellationToken);

        if (telefoneDuplicado)
            throw new InvalidOperationException(
                "Este telefone já está cadastrado.");

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var emailDuplicado =
                await clienteRepository.EmailExisteParaOutroClienteAsync(
                    request.Email,
                    request.ClienteId,
                    cancellationToken);

            if (emailDuplicado)
                throw new InvalidOperationException(
                    "Este e-mail já está cadastrado.");
        }

        cliente.AtualizarPerfil(
            request.Nome,
            request.Telefone,
            request.Email);

        await clienteRepository.SalvarAlteracoesAsync(
            cancellationToken);

        return true;
    }
}