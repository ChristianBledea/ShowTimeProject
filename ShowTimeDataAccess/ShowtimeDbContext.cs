using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Configurations;

namespace ShowTime.DataAccess
{
    public class ShowtimeDbContext : DbContext
    {
        public ShowtimeDbContext(DbContextOptions<ShowtimeDbContext> options)
            : base(options)
        {
        }
        public DbSet<Festival> Festivals { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Lineup> Lineups { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowtimeDbContext).Assembly);
            new FestivalConfiguration().Configure(modelBuilder.Entity<Festival>());
            new ArtistConfiguration().Configure(modelBuilder.Entity<Artist>());
            new LineupConfiguration().Configure(modelBuilder.Entity<Lineup>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new BookingConfiguration().Configure(modelBuilder.Entity<Booking>());
            new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
            new TicketConfiguration().Configure(modelBuilder.Entity<Ticket>());
        }
    }
}
