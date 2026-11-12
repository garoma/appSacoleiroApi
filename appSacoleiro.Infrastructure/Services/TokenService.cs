
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using appSacoleiro.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace appSacoleiro.Infrastructure.Services;

public sealed class TokenService(IConfiguration configuration)
    : ITokenService
{
    public (string Token, DateTime ExpiraEmUtc) GerarToken(
        UsuarioAutenticavel usuario)
    {
        var jwtKey = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "A configuração Jwt:Key não foi encontrada.");

        var issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "A configuração Jwt:Issuer não foi encontrada.");

        var audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "A configuração Jwt:Audience não foi encontrada.");

        if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
        {
            throw new InvalidOperationException(
                "A chave JWT deve possuir pelo menos 32 bytes.");
        }

        var minutos = int.TryParse(
            configuration["Jwt:ExpirationInMinutes"],
            out var valor) && valor > 0
                ? valor
                : 60;

        var agora = DateTime.UtcNow;
        var expiraEmUtc = agora.AddMinutes(minutos);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Perfil)
        };

        if (!string.IsNullOrWhiteSpace(usuario.Email))
        {
            claims = claims.Append(
                new Claim(ClaimTypes.Email, usuario.Email)).ToArray();
        }

        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var credenciais = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: agora,
            expires: expiraEmUtc,
            signingCredentials: credenciais);

        return (
            new JwtSecurityTokenHandler().WriteToken(token),
            expiraEmUtc
        );
    }
}
