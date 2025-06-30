using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configurations
{
    public class FestivalConfiguration : IEntityTypeConfiguration<Festival>
    {
        public void Configure(EntityTypeBuilder<Festival> builder)
        {
            builder.ToTable("Festivals");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(f => f.Location)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(f => f.StartDate)
                .IsRequired();
            builder.Property(f => f.EndDate)
                .IsRequired();
            builder.Property(f => f.Capacity)
                .IsRequired();
            builder.Property(f => f.SplashArt)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasMany(f => f.Artists)
                .WithMany(l => l.Festivals)
                .UsingEntity<Lineup>();
        }
    }
}
