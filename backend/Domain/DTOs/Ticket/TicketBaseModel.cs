using System;
public abstract class TicketBaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Statusid { get; set; }
}