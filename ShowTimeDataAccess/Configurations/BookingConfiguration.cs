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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(b => new { b.FestivalId, b.UserId });
            builder.HasOne(b => b.Festival)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FestivalId);
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);
            builder.Property(b => b.Type)
                .IsRequired()
                .HasMaxLength(24);
            builder.Property(b => b.Price)
                .IsRequired();
        }
    }
}
