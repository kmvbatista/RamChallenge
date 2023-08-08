using System;
public abstract class TicketBaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TicketCategory Category { get; set; }
    public DateTime Deadline { get; set; }
    public Guid StatusId { get; set; }
}