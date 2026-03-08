using F1ZoneLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.DATA
{
    public class F1ZoneDbContext : DbContext
    {
        public F1ZoneDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public F1ZoneDbContext()
        {
            
        }

        public DbSet<Circuits> Circuits { get; set; }
        public DbSet<Driver_contracts> Driver_contracts { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<Engines> Engines { get; set; }
        public DbSet<Fastest_laps> Fastest_laps { get; set; }
        public DbSet<Pit_stops> Pit_stops { get; set; }
        public DbSet<Qualifying_results> Qualifying_results { get; set; }
        public DbSet<Race_results> Race_results { get; set; }
        public DbSet<Races> Races { get; set; }
        public DbSet<Seasons> Seasons { get; set; }
        public DbSet<Sponsors> Sponsors { get; set; }
        public DbSet<Team_sponsors> Team_sponsors { get; set; }
        public DbSet<Teams> Teams { get; set; }
    }
}
