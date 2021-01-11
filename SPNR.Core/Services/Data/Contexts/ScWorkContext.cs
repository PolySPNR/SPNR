using Microsoft.EntityFrameworkCore;
using SPNR.Core.Misc;
using SPNR.Core.Models;

namespace SPNR.Core.Services.Data.Contexts
{
    public class ScWorkContext : DbContext
    {
        public DbSet<ScientificWork> Works { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbLocation = EnvVar.Get("SPNR_SQLITE_LOCATION", "./spnr.db");
            optionsBuilder.UseSqlite($"Data Source={dbLocation.Value}");
        }
    }
}