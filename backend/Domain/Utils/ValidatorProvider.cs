using FluentValidation;

public class ValidatorProvider
{
    public static IValidator GetValidator<T>(T entity)
    {
        if (entity is Ticket)
            return new TicketValidator();
        if (entity is Status)
            return new StatusValidator();
        if (entity is Link)
            return new LinkValidator();
        return null;
    }
}
