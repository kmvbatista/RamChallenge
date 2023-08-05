using Infraestructure.Context;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(MainContext dbContext) : base(dbContext)
    {
    }
}