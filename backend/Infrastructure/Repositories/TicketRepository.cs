using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructure.Context;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(MainContext dbContext) : base(dbContext)
    {


    }

    public new async Task<Ticket> GetById(Guid id)
    {
        return await _dbContext.Set<Ticket>()
            .Include(ticket => ticket.Links)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public new async Task<IList<Ticket>> GetAll()
    {
        return await _dbContext.Set<Ticket>().Include(ticket => ticket.Links).ToListAsync();
    }
}