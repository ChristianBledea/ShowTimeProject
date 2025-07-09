using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Price)  // Changed from price to Price
                .IsRequired();

            builder.Property(t => t.Type)
                .IsRequired();

            // Configure one-to-one relationship with Booking
            builder.HasOne(t => t.Booking)
                .WithOne(b => b.Ticket);
        }
    }
}
