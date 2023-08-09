using System;
using System.Collections.Generic;

public class Ticket : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TicketCategory Category { get; set; }
    public IList<Link> Links { get; set; }
    public virtual Status Status { get; set; }
    public Guid StatusId { get; set; }
    public void Update(TicketRequestModel requestModel)
    {
        this.Name = requestModel.Name;
        this.Deadline = requestModel.Deadline;
        this.Description = requestModel.Description;
        this.StatusId = requestModel.StatusId;
        this.Category = requestModel.Category;
        this.Validate();
    }

    public void UpdateLinks(string linkUrl)
    {
        var newLink = new Link(this.Id, linkUrl);
        if (this.Links is null)
        {
            this.Links = new List<Link>();
        }
        this.Links.Add(newLink);
    }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }
}