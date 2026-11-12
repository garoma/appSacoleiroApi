
using appSacoleiro.Application.Interfaces;
using MediatR;

namespace appSacoleiro.Application.Clientes.Queries;

public sealed record ObterClientePorIdQuery(Guid ClienteId)
    : IRequest<ObterClientePorIdResult?>;

public sealed record ObterClientePorIdResult(
    Guid Id,
    string Nome,
    string Telefone,
    string? Email,
    string Status);

public sealed class ObterClientePorIdQueryHandler
    : IRequestHandler<ObterClientePorIdQuery, ObterClientePorIdResult?>
{
    private readonly IClienteRepository _clienteRepository;

    public ObterClientePorIdQueryHandler(
        IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ObterClientePorIdResult?> Handle(
        ObterClientePorIdQuery request,
        CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            request.ClienteId,
            cancellationToken);

        if (cliente is null)
            return null;

        return new ObterClientePorIdResult(
            cliente.Id,
            cliente.Nome,
            cliente.Telefone,
            cliente.Email,
            cliente.Status);
    }
}
