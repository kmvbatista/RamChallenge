using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITicketService
{
    Task<Ticket> Create(Ticket ticket);
    Task<IList<Ticket>> GetAll();
    Task<Ticket> GetById(Guid id);
    Task<Ticket> Update(Guid id, TicketRequestModel ticket);
    Task Delete(Guid id);
    Task<Ticket> AddTicketLink(Guid id, string linkUrl);
    Task RemoveTicketLink(Guid linkId);
}
