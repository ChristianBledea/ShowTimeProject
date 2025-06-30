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
    public  class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artists");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.Image)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(a => a.Genre)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasMany(a => a.Festivals)
                .WithMany(l => l.Artists)
                .UsingEntity<Lineup>();
        }
    }
}
