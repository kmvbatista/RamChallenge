using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITicketService
{
    Task Create(TicketRequestModel requestModel);
    Task<IEnumerable<TicketResponseModel>> GetAll();
    Task<TicketResponseModel> GetById(Guid id);
    Task Update(Guid id, TicketRequestModel ticketRequestModel);
    Task Delete(Guid id);
}
