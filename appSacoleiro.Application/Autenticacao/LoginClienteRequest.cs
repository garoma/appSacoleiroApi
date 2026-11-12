
using FluentValidation;

namespace appSacoleiro.Application.Autenticacao;

public sealed record LoginClienteRequest(
    string Identificador,
    string Senha);

public sealed class LoginClienteValidator
    : AbstractValidator<LoginClienteRequest>
{
    public LoginClienteValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Senha)
            .NotEmpty()
            .MaximumLength(200);
    }
}
