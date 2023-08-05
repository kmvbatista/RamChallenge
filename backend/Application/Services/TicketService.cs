using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

public class TicketService : ITicketService
{
    private ITicketRepository _ticketRepository;
    private IStatusService _status_service;
    private TicketMapper _ticketMapper;


    public TicketService(ITicketRepository ticketRepository, IStatusService status_service)
    {
        _ticketRepository = ticketRepository;
        _ticketMapper = new TicketMapper();
        _status_service = status_service;
    }
    public async Task<Ticket> Create(TicketRequestModel requestModel)
    {
        Ticket ticket = MapTicketFromRequestModel(requestModel);
        Status ticketStatus = await _status_service.GetById(new Guid(requestModel.Statusid));
        ticket.Status = ticketStatus;
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
        return MapTicketToResponseModel(ticket);
    }
    public async Task Update(Guid id, TicketRequestModel ticketRequestModel)
    {
        var ticketToUpdate = await _ticketRepository.GetById(id);
        ticketToUpdate.Update(ticketToUpdate, ticketRequestModel);
        await _ticketRepository.Update(ticketToUpdate);
    }
    public async Task Delete(Guid id)
    {
        await _ticketRepository.Delete(id);
    }
    private TicketResponseModel MapTicketToResponseModel(Ticket ticket) => _ticketMapper.MapToResponseModel(ticket);
    private Ticket MapTicketFromRequestModel(TicketRequestModel ticket)
    {
        var mappedTicket = _ticketMapper.MapFromRequestModel(ticket);
        mappedTicket.Links = ticket.Links.Select(x => new LinkMapper().MapFromRequestModel(x)).ToList();
        mappedTicket.Validate();
        return mappedTicket;
    }
}