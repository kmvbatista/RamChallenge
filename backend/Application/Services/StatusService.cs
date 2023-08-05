using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

public class StatusService : IStatusService
{
    private IStatusRepository _statusRepository;
    private StatusMapper _statusMapper;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
        _statusMapper = new StatusMapper();
    }
    public async Task<Status> Create(StatusRequestModel requestModel)
    {
        var lastStatusByOrder = _statusRepository.GetStatusByLatestOrder();
        Status status = MapStatusFromRequestModel(requestModel);
        status.Order = lastStatusByOrder.Order + 1;
        return await _statusRepository.Create(status);
    }
    public async Task<Status> GetById(Guid id)
    {
        var status = await _statusRepository.GetById(id);
        return status;
    }
    public async Task<IList<StatusResponseModel>> GetAll()
    {
        var tasks = await _statusRepository.GetAll();
        var statusResponse = tasks.Select(MapStatusToResponseModel).ToList();
        return statusResponse;
    }
    public async Task Update(Guid id, StatusRequestModel statusRequestModel)
    {
        var statusToUpdate = await _statusRepository.GetById(id);
        statusToUpdate.Update(statusToUpdate, statusRequestModel);
        await _statusRepository.Update(statusToUpdate);
    }
    public async Task Delete(Guid id)
    {
        await _statusRepository.Delete(id);
    }

    private StatusResponseModel MapStatusToResponseModel(Status status) => _statusMapper.MapToResponseModel(status);
    private Status MapStatusFromRequestModel(StatusRequestModel status) => _statusMapper.MapFromRequestModel(status);
}