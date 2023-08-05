using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStatusService
{
    Task<Status> Create(StatusRequestModel requestModel);
    Task<IList<StatusResponseModel>> GetAll();
    Task<Status> GetById(Guid id);
    Task Update(Guid id, StatusRequestModel statusRequestModel);
    Task Delete(Guid id);
}
