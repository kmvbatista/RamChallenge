using System;
using System.Collections.Generic;

public class TicketRequestModel : TicketBaseModel, IRequestModel
{
    public IList<LinkRequestModel> Links { get; set; }
    public Status Status;
}