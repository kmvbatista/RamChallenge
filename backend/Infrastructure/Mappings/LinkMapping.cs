using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class LinkMapping : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.Property(t => t.Url).HasColumnName(nameof(Link.Url)).IsRequired();
        builder.HasOne<Ticket>(l => l.Ticket).WithMany(t => t.Links).HasForeignKey(t => t.TicketId);
    }
}