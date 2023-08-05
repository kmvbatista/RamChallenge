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
    public async Task Create(TicketRequestModel requestModel)
    {
        Status ticketStatus = await _status_service.GetById(requestModel.StatusId);
        requestModel.Status = ticketStatus;
        Ticket ticket = MapTicketFromRequestModel(requestModel);
        await _ticketRepository.Create(ticket);
    }
    public async Task<IEnumerable<TicketResponseModel>> GetAll()
    {
        var tasks = await _ticketRepository.GetAll();
        var ticketResponse = tasks.Select(MapTicketToResponseModel);
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