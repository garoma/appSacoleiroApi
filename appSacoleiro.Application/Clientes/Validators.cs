using appSacoleiro.Application.Clientes.Commands;
using FluentValidation;

namespace appSacoleiro.Application.Clientes.Validators;

public sealed class AtualizarPerfilClienteCommandValidator
    : AbstractValidator<AtualizarPerfilClienteCommand>
{
    public AtualizarPerfilClienteCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MaximumLength(150)
            .WithMessage("O nome deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage("O telefone é obrigatório.")
            .MaximumLength(20)
            .WithMessage("Telefone inválido.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Informe um e-mail válido.");
    }
}