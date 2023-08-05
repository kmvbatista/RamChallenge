using AutoMapper;

public class StatusMapper
                : BaseMapper<Status, StatusRequestModel, StatusResponseModel>
{
    public StatusMapper()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Status, StatusResponseModel>();
            cfg.CreateMap<StatusRequestModel, Status>();
        }).CreateMapper();
    }
}