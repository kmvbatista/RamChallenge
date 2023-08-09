using System;
using System.Threading.Tasks;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task RemoveTicketLink(Guid linkId);
}