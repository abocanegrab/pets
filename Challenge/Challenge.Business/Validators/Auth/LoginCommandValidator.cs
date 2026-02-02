using Challenge.Business.Features.Auth.Login;
using FluentValidation;

namespace Challenge.Business.Validators.Auth;

/// <summary>
/// Validador para LoginCommand
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .MaximumLength(50).WithMessage("El nombre de usuario no puede exceder 50 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(3).WithMessage("La contraseña debe tener al menos 3 caracteres");
    }
}
