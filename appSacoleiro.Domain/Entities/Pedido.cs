
namespace appSacoleiro.Domain.Entities;

public sealed class Pedido
{
    public Guid Id { get; private set; }

    public Guid ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }

    public Guid ProdutoId { get; private set; }
    public Produto? Produto { get; private set; }

    public int Quantidade { get; private set; }

    public decimal PrecoUnitario { get; private set; }

    public decimal Total { get; private set; }

    public string Status { get; private set; } = "aguardando_pagamento";

    public DateTime CriadoEmUtc { get; private set; }

    private Pedido() { }

    public Pedido(Guid clienteId, Produto produto, int quantidade)
    {
        if (clienteId == Guid.Empty)
            throw new ArgumentException("Cliente inválido.");

        if (produto is null)
            throw new ArgumentException("Produto não informado.");

        if (!produto.Ativo)
            throw new InvalidOperationException(
                "Este produto não está disponível para compra.");

        if (quantidade <= 0)
            throw new ArgumentException(
                "A quantidade deve ser maior que zero.");

        Id = Guid.NewGuid();
        ClienteId = clienteId;
        ProdutoId = produto.Id;
        Quantidade = quantidade;
        PrecoUnitario = produto.Preco;
        Total = PrecoUnitario * quantidade;
        Status = "aguardando_pagamento";
        CriadoEmUtc = DateTime.UtcNow;
    }

    public void AlterarStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status do pedido inválido.");

        if (Status == "entregue" || Status == "cancelado")
            throw new InvalidOperationException(
                "Este pedido já foi finalizado.");

        var statusPermitidos = new[]
        {
        "pago",
        "entregue",
        "cancelado"
    };

        if (!statusPermitidos.Contains(status))
            throw new ArgumentException("Status do pedido inválido.");

        if (Status == "aguardando_pagamento" &&
            status == "entregue")
        {
            throw new InvalidOperationException(
                "O pedido precisa ser pago antes de ser entregue.");
        }

        Status = status;
    }

}
