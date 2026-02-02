using Challenge.Business.Features.Client.Create;
using FluentValidation;

namespace Challenge.Business.Validators.Client;

/// <summary>
/// Validador para CreateClientCommand
/// </summary>
public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido")
            .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El teléfono es requerido")
            .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres")
            .Matches(@"^[\d\-\+\(\)\s]+$").WithMessage("El teléfono solo puede contener números, espacios, guiones, + y paréntesis");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("El email no tiene un formato válido")
            .MaximumLength(255).When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("El email no puede exceder 255 caracteres");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección es requerida")
            .MaximumLength(500).WithMessage("La dirección no puede exceder 500 caracteres");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerida")
            .MaximumLength(100).WithMessage("La ciudad no puede exceder 100 caracteres");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("El estado es requerido")
            .MaximumLength(50).WithMessage("El estado no puede exceder 50 caracteres");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("El código postal es requerido")
            .MaximumLength(10).WithMessage("El código postal no puede exceder 10 caracteres");
    }
}
