
using appSacoleiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace appSacoleiro.Infrastructure.Persistence;

public sealed class appSacoleiroDbContext(
    DbContextOptions<appSacoleiroDbContext> options)
    : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();

    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(appSacoleiroDbContext).Assembly);
    }
}
