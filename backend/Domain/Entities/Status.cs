public class Status : BaseEntity
{
    public string Name { get; set; }
    public int Order { get; set; }

    public void Update(Status ticket, StatusRequestModel requestModel)
    {
        ticket.Name = requestModel.Name;
        ticket.Order = requestModel.Order;
        ticket.Validate();
    }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }
}