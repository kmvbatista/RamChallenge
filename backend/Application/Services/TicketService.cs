using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

public class TicketService : ITicketService
{
    private ITicketRepository _ticketRepository;


    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    public async Task<Ticket> Create(Ticket ticket)
    {
        return await _ticketRepository.Create(ticket);
    }
    public async Task<IList<Ticket>> GetAll()
    {
        return await _ticketRepository.GetAll();
    }
    public async Task<Ticket> GetById(Guid id)
    {
        return await _ticketRepository.GetById(id);
    }
    public async Task<Ticket> Update(Guid id, TicketRequestModel ticketRequestModel)
    {
        var ticketToUpdate = await _ticketRepository.GetById(id);
        ticketToUpdate.Update(ticketRequestModel);
        return await _ticketRepository.Update(ticketToUpdate);
    }
    public async Task Delete(Guid id)
    {
        await _ticketRepository.Delete(id);
    }
    public async Task<Ticket> AddTicketLink(Guid id, string linkUrl)
    {
        var ticketToUpdate = await _ticketRepository.GetById(id);
        ticketToUpdate.UpdateLinks(linkUrl);
        return await _ticketRepository.Update(ticketToUpdate);
    }
    public async Task RemoveTicketLink(Guid linkId)
    {
        await _ticketRepository.RemoveTicketLink(linkId);
    }


}