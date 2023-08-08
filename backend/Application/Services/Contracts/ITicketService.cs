using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITicketService
{
    Task<Ticket> Create(TicketRequestModel requestModel);
    Task<IList<TicketResponseModel>> GetAll();
    Task<TicketResponseModel> GetById(Guid id);
    Task<TicketResponseModel> Update(Guid id, TicketRequestModel ticketRequestModel);
    Task Delete(Guid id);
    Task<TicketResponseModel> AddTicketLink(Guid id, string linkUrl);
}
