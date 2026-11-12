
namespace appSacoleiro.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Telefone { get; private set; } = string.Empty;

    public string SenhaHash { get; private set; } = string.Empty;

    public string? Email { get; private set; }

    public string Status { get; private set; } = "Ativo";

    public DateTime CriadoEmUtc { get; private set; }

    protected Cliente()
    {
    }

    public Cliente(
        string nome,
        string telefone,
        string? email = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException(
                "O nome do cliente é obrigatório.", nameof(nome));

        if (string.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException(
                "O telefone do cliente é obrigatório.", nameof(telefone));

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Telefone = telefone.Trim();
        Email = string.IsNullOrWhiteSpace(email)
            ? null
            : email.Trim();
        Status = "Ativo";
        CriadoEmUtc = DateTime.UtcNow;
    }

    public void DefinirSenhaHash(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException(
                "O hash da senha é obrigatório.", nameof(senhaHash));

        SenhaHash = senhaHash;
    }

    public void AtualizarPerfil(
    string nome,
    string telefone,
    string? email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("O telefone é obrigatório.");

        Nome = nome.Trim();
        Telefone = telefone.Trim();

        Email = string.IsNullOrWhiteSpace(email)
            ? null
            : email.Trim();
    }
}
