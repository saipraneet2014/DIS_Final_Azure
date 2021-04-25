 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DIS_Final_Azure.Models;

namespace DIS_Final_Azure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Fuel_Stations> Fuel_Stations { get; set; }
        public DbSet<EVs> EVs { get; set; }
        //public DbSet<Federal_Agency_Stations> Federal_Agency_Station { get; set; }
        //public DbSet<Federal_Agency> Federal_Agencies { get; set; }
        //public DbSet<CreateStation> CreateStations { get; set; }
     

    }
}
