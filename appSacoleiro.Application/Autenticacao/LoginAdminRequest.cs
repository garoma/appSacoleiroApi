
using FluentValidation;

namespace appSacoleiro.Application.Autenticacao;

public sealed record LoginAdminRequest(
    string Email,
    string Senha);

public sealed class LoginAdminValidator
    : AbstractValidator<LoginAdminRequest>
{
    public LoginAdminValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Senha)
            .NotEmpty()
            .MaximumLength(200);
    }
}
