public class Link : BaseEntity
{
    public string Url { get; set; }

    public override void Validate()
    {
        ValidateFromSubClass(this);
    }

}