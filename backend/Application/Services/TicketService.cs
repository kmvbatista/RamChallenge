using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

public class TicketService : ITicketService
{
    private ITicketRepository _ticketRepository;
    private IStatusService _status_service;
    private TicketMapper _ticketMapper;
    private LinkMapper _linkMapper;


    public TicketService(ITicketRepository ticketRepository, IStatusService status_service)
    {
        _ticketRepository = ticketRepository;
        _ticketMapper = new TicketMapper();
        _linkMapper = new LinkMapper();
        _status_service = status_service;
    }
    public async Task<Ticket> Create(TicketRequestModel requestModel)
    {
        Ticket ticket = MapTicketFromRequestModel(requestModel);
        return await _ticketRepository.Create(ticket);
    }
    public async Task<IList<TicketResponseModel>> GetAll()
    {
        var tasks = await _ticketRepository.GetAll();
        var ticketResponse = tasks.Select(MapTicketToResponseModel).ToList();
        return ticketResponse;
    }
    public async Task<TicketResponseModel> GetById(Guid id)
    {
        var ticket = await _ticketRepository.GetById(id);
        var result = MapTicketToResponseModel(ticket);
        return result;
    }
    public async Task<TicketResponseModel> Update(Guid id, TicketRequestModel ticketRequestModel)
    {
        var ticketToUpdate = await _ticketRepository.GetById(id);
        ticketToUpdate.Update(ticketRequestModel);
        var updatedTicket = await _ticketRepository.Update(ticketToUpdate);
        return MapTicketToResponseModel(updatedTicket);
    }
    public async Task Delete(Guid id)
    {
        await _ticketRepository.Delete(id);
    }
    public async Task<TicketResponseModel> AddTicketLink(Guid id, string linkUrl)
    {
        var ticketToUpdate = await _ticketRepository.GetById(id);
        ticketToUpdate.AddLink(linkUrl);
        var updatedTicket = await _ticketRepository.Update(ticketToUpdate);
        var result = MapTicketToResponseModel(updatedTicket);
        return result;
    }
    private TicketResponseModel MapTicketToResponseModel(Ticket ticket)
    {
        var mappedTicket = _ticketMapper.MapToResponseModel(ticket);
        var ticketLinks = ticket?.Links?.Select(link => _linkMapper.MapToResponseModel(link))?.ToList();
        mappedTicket.links = ticketLinks;
        return mappedTicket;
    }

    private Ticket MapTicketFromRequestModel(TicketRequestModel ticket)
    {
        var mappedTicket = _ticketMapper.MapFromRequestModel(ticket);
        mappedTicket.Links = ticket.Links.Select(x => new LinkMapper().MapFromRequestModel(x)).ToList();
        mappedTicket.Validate();
        return mappedTicket;
    }
}