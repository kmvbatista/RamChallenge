using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStatusService
{
    Task Create(StatusRequestModel requestModel);
    Task<IEnumerable<StatusResponseModel>> GetAll();
    Task<Status> GetById(Guid id);
    Task Update(Guid id, StatusRequestModel statusRequestModel);
    Task Delete(Guid id);
}
