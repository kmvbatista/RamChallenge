using System;
using System.Collections.Generic;

public class TicketResponseModel : TicketBaseModel, IResponseModel
{
    public Guid Id { get; set; }
    public List<LinkResponseModel> links;
}