using FluentValidation;

public class StatusValidator : AbstractValidator<Status>
{
    public StatusValidator()
    {
        var x = RuleFor(status => status)
            .NotNull().WithMessage("Status cannot be null");
        RuleFor(status => status.Name.Length).GreaterThan(3).WithMessage("Status names have to be at least 3 characters long");
    }

}