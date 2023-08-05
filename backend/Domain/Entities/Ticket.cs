using System;
using System.Collections.Generic;

public class Ticket : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public virtual IList<Link> Links { get; set; }
    public virtual Status Status { get; set; }
    public Guid StatusId { get; set; }
    public void Update(Ticket ticket, TicketRequestModel requestModel)
    {
        ticket.Name = requestModel.Name;
        ticket.Deadline = requestModel.Deadline;
        ticket.Description = requestModel.Description;
        ticket.Validate();
    }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }
}