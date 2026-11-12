using appSacoleiro.Application.Interfaces;
using appSacoleiro.Application.Produtos.DTOs;
using MediatR;

namespace appSacoleiro.Application.Produtos.ListarProdutos;

public sealed class ListarProdutosHandler(
    IProdutoRepository produtoRepository)
    : IRequestHandler<ListarProdutosQuery, IReadOnlyList<ProdutoListagemDto>>
{
    public async Task<IReadOnlyList<ProdutoListagemDto>> Handle(
        ListarProdutosQuery request,
        CancellationToken cancellationToken)
    {
        return await produtoRepository.ListarAsync(
            request.Categoria,
            cancellationToken);
    }
}