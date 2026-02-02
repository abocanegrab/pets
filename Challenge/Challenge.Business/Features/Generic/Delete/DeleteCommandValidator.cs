using Challenge.Core.Interfaces;
using FluentValidation;

namespace Challenge.Business.Features.Generic.Delete;

/// <summary>
/// Validador genérico para DeleteCommand
/// </summary>
public class DeleteCommandValidator<TEntity> : AbstractValidator<DeleteCommand<TEntity>>
    where TEntity : class, IIdentifier
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor que 0");
    }
}
