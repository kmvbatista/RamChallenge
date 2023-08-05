using System;
using System.Collections.Generic;

public class TicketRequestModel : TicketBaseModel, IRequestModel
{
    public IList<LinkRequestModel> Links;
    public Guid StatusId;
    public Status Status;
}