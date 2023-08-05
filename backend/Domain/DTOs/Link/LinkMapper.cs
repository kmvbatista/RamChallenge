using AutoMapper;

public class LinkMapper
                : BaseMapper<Link, LinkRequestModel, LinkResponseModel>
{
    public LinkMapper()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Link, LinkResponseModel>();
            cfg.CreateMap<LinkRequestModel, Link>();
        }).CreateMapper();
    }
}