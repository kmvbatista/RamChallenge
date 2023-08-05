using System;
public abstract class StatusBaseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
}