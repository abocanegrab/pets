using Challenge.Business.Features.Walk.Update;
using FluentValidation;

namespace Challenge.Business.Validators.Walk;

/// <summary>
/// Validador para UpdateWalkCommand
/// </summary>
public class UpdateWalkCommandValidator : AbstractValidator<UpdateWalkCommand>
{
    public UpdateWalkCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID del paseo debe ser mayor que 0");

        RuleFor(x => x.DogId)
            .GreaterThan(0).WithMessage("El ID del perro debe ser mayor que 0");

        RuleFor(x => x.WalkedByUserId)
            .GreaterThan(0).WithMessage("El ID del usuario debe ser mayor que 0");

        RuleFor(x => x.WalkDate)
            .NotEmpty().WithMessage("La fecha del paseo es requerida")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha del paseo no puede ser en el futuro");

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("La duración debe ser mayor que 0 minutos")
            .LessThanOrEqualTo(480).WithMessage("La duración no puede exceder 8 horas (480 minutos)");

        RuleFor(x => x.Distance)
            .GreaterThan(0).WithMessage("La distancia debe ser mayor que 0")
            .LessThanOrEqualTo(100).WithMessage("La distancia no puede exceder 100 km");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Notes))
            .WithMessage("Las notas no pueden exceder 1000 caracteres");
    }
}
