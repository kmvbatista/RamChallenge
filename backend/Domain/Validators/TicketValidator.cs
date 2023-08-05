using System;
using FluentValidation;
public class TicketValidator : AbstractValidator<Ticket>
{
    public TicketValidator()
    {
        RuleFor(task => task.Name.Length).GreaterThan(3).WithMessage("Task names have to be at least 3 characters long");
        RuleFor(task => task.Status).SetValidator(new StatusValidator());
        RuleForEach(task => task.Links)
            .SetValidator(new LinkValidator());
        RuleFor(task => task.Deadline)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Deadline must be in the future");

    }
}