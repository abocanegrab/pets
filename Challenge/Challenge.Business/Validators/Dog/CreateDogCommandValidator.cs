using Challenge.Business.Features.Dog.Create;
using FluentValidation;

namespace Challenge.Business.Validators.Dog;

/// <summary>
/// Validador para CreateDogCommand
/// </summary>
public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
{
    public CreateDogCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor que 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del perro es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Breed)
            .NotEmpty().WithMessage("La raza es requerida")
            .MaximumLength(100).WithMessage("La raza no puede exceder 100 caracteres");

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).WithMessage("La edad debe ser mayor o igual a 0")
            .LessThanOrEqualTo(30).WithMessage("La edad no puede exceder 30 años");

        RuleFor(x => x.Weight)
            .GreaterThan(0).When(x => x.Weight.HasValue)
            .WithMessage("El peso debe ser mayor que 0")
            .LessThanOrEqualTo(200).When(x => x.Weight.HasValue)
            .WithMessage("El peso no puede exceder 200 kg");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.SpecialInstructions))
            .WithMessage("Las instrucciones especiales no pueden exceder 1000 caracteres");
    }
}
