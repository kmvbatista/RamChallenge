using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasMany<Link>(c => c.Links).WithOne(c => c.Ticket).HasForeignKey(c => c.TicketId);
        builder.Property(t => t.Deadline).HasColumnName(nameof(Ticket.Deadline)).IsRequired(); ;
        builder.Property(t => t.Name).HasColumnName(nameof(Ticket.Name)).IsRequired(); ;
        builder.Property(t => t.Description).HasColumnName(nameof(Ticket.Description)).IsRequired(); ;
        builder.Property(t => t.Category).HasColumnName(nameof(Ticket.Category));
        builder.HasOne<Status>(t => t.Status).WithMany();
    }
}