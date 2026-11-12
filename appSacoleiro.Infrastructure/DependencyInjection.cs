
using appSacoleiro.Application.Autenticacao;
using appSacoleiro.Application.Interfaces;
using appSacoleiro.Domain.Entities;
using appSacoleiro.Infrastructure.Persistence;
using appSacoleiro.Infrastructure.Repositories;
using appSacoleiro.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace appSacoleiro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("appSacoleiro")
            ?? throw new InvalidOperationException(
                "A conexão appSacoleiro não foi configurada.");

        services.AddDbContext<appSacoleiroDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher<AdminUser>, PasswordHasher<AdminUser>>();
        services.AddScoped<IPasswordHasher<Cliente>, PasswordHasher<Cliente>>();
        services.AddScoped<ILoginAdminService, LoginAdminService>();
        services.AddScoped<ILoginClienteService, LoginClienteService>();
        services.AddScoped<AdminSeeder>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }
}
