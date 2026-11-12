
namespace appSacoleiro.Domain.Entities;

public sealed class AdminUser
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    private AdminUser()
    {
    }

    public AdminUser(string nome, string email, string passwordHash)
    {
        Nome = nome;
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
    }

    public void AtualizarPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
}
