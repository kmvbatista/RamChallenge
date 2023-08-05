using System;

public class Link : BaseEntity
{
    public Guid TicketId { get; }
    public virtual Ticket Ticket { get; }
    public string Url { get; set; }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }

}