using appSacoleiro.Application.Interfaces;
using MediatR;

namespace appSacoleiro.Application.Clientes.Queries;

public sealed record ListarClientesQuery
    : IRequest<IReadOnlyList<ListarClientesResult>>;

public sealed record ListarClientesResult(
    Guid Id,
    string Nome,
    string Telefone,
    string? Email,
    string Status);

public sealed class ListarClientesQueryHandler
    : IRequestHandler<ListarClientesQuery, IReadOnlyList<ListarClientesResult>>
{
    private readonly IClienteRepository _clienteRepository;

    public ListarClientesQueryHandler(
        IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IReadOnlyList<ListarClientesResult>> Handle(
        ListarClientesQuery request,
        CancellationToken cancellationToken)
    {
        var clientes = await _clienteRepository.ListarTodosAsync(
            cancellationToken);

        return clientes.Select(cliente => new ListarClientesResult(
            cliente.Id,
            cliente.Nome,
            cliente.Telefone,
            cliente.Email,
            cliente.Status
        )).ToList();
    }
}