
using appSacoleiro.Application.Produtos.DTOs;
using MediatR;

namespace appSacoleiro.Application.Produtos.ListarProdutos;

public sealed record ListarProdutosQuery(
    string? Categoria = null
) : IRequest<IReadOnlyList<ProdutoListagemDto>>;
