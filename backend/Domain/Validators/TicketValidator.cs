using System;
using FluentValidation;
public class TicketValidator : AbstractValidator<Ticket>
{
    public TicketValidator()
    {
        RuleFor(task => task.Name.Length).NotNull().GreaterThan(3).WithMessage("Task names have to be at least 3 characters long");
        RuleFor(task => task.StatusId).NotNull().WithMessage("Status id cannot be null");
        // RuleFor(task => task.Status).SetValidator(new StatusValidator());
        // RuleForEach(task => task.Links)
        //     .SetValidator(new LinkValidator());
        RuleFor(task => task.Deadline).NotNull()
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Deadline must be in the future");

    }
}