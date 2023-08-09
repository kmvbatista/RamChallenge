using System;

public class Link : BaseEntity
{
    public Link()
    {

    }
    public Link(Guid ticketId, string url)
    {
        this.TicketId = ticketId;
        this.Url = url;
    }
    public Guid TicketId { get; }
    public virtual Ticket Ticket { get; }
    public string Url { get; set; }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }

}