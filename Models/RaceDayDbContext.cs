using System.Collections.Generic;
using RaceDay_POE.Models;
using Microsoft.EntityFrameworkCore;

namespace RaceDay_POE.Models
{
    public class RaceDayDbContext: DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options) : base(options) { }

        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Results> Results { get; set; }
    }
}
