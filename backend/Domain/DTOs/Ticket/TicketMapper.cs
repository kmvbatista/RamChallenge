using System.Collections.Generic;
using System.Linq;
using AutoMapper;

public class TicketMapper
                : BaseMapper<Ticket, TicketRequestModel, TicketResponseModel>
{
    public TicketMapper()
    {
        var link_mapper = new LinkMapper();
        _mapper = new MapperConfiguration(cfg =>
        {
            // cfg.CreateMap<Ticket, TicketResponseModel>().ForMember(ticket => ticket.links, map => map.MapFrom(tick => tick.Links.Select(link => new LinkMapper().MapToResponseModel(link))));
            cfg.CreateMap<Ticket, TicketResponseModel>().ForMember(ticket => ticket.links, map => map.MapFrom(tick => tick.Links.Select(link => link_mapper.MapToResponseModel(link))));
            cfg.CreateMap<TicketRequestModel, Ticket>().ForMember(ticket => ticket.Links, map => map.MapFrom(x => x.Links.Select(y => link_mapper.MapFromRequestModel(y))));
            // .ForMember(ticket => ticket.Links, map =>  map.Links.Select(x => new LinkMapper().MapFromRequestModel(x))));
        }).CreateMapper();
    }
}


