using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructure.Context;
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
    public async Task RemoveTicketLink(Guid linkId)
    {
        var entity = await _dbContext.Set<Link>().AsNoTracking()
           .FirstOrDefaultAsync(e => e.Id == linkId);
        _dbContext.Set<Link>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}