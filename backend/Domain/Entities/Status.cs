using System.Collections.Generic;

public class Status : BaseEntity
{
    public string Name { get; set; }
    public int Order { get; set; }
    public virtual IList<Ticket> Tickets { get; }

    public void Update(Status ticket, StatusRequestModel requestModel)
    {
        ticket.Name = requestModel.Name;
        ticket.Validate();
    }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }
}