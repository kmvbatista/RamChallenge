using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidationResult = FluentValidation.Results.ValidationResult;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    [NotMapped]
    public bool IsValid { get; private set; }
    [NotMapped]
    public ValidationResult ValidationResult { get; private set; }

    protected bool ValidateFromSubClass<T>(T instance)
    {
        var validator = (AbstractValidator<T>)ValidatorProvider.GetValidator(this);
        ValidationResult = validator.Validate(instance);
        IsValid = ValidationResult.IsValid;
        if (!IsValid)
        {
            throw new FluentValidation.ValidationException(ValidationResult.ToString());
        }
        return IsValid;
    }

    public abstract void Validate();
}
