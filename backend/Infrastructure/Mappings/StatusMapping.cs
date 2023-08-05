using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class StatusMapping : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasMany<Ticket>(c => c.Tickets).WithOne(c => c.Status).HasForeignKey(x => x.StatusId);
        builder.Property(s => s.Name).IsRequired();
        builder.HasIndex(s => s.Order).IsUnique();
    }
}