using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPNR.Core.Misc;
using SPNR.Core.Models.AuthorInfo;
using SPNR.Core.Models.Works;
using SPNR.Core.Models.Works.Fields;
using SPNR.Core.Models.Works.PublishData;

namespace SPNR.Core.Services.Data.Contexts
{
    public class ScWorkContext : DbContext
    {
        public ScWorkContext(DbContextOptions<ScWorkContext> options) : base(options)
        {
            
        }

        public DbSet<ScientificWork> Works { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ELibInfo> ELibFields { get; set; }
        public DbSet<JournalPublish> JournalPublishes { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbLocation = EnvVar.Get("SPNR_SQLITE_LOCATION", "./spnr.db");
            optionsBuilder.UseSqlite($"Data Source={dbLocation.Value}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ELibInfo>()
                .Property(e => e.RawMetrics)
                .HasConversion(d => JsonConvert.SerializeObject(d),
                    d => JsonConvert.DeserializeObject<Dictionary<string, string>>(d));
            
            modelBuilder.Entity<ScientificWork>()
                .Property(e => e.PublicationInfo)
                .HasConversion(d => JsonConvert.SerializeObject(d),
                    d => JsonConvert.DeserializeObject<Dictionary<string, string>>(d));
            
            modelBuilder.Entity<ScientificWork>()
                .Property(e => e.PublicationMeta)
                .HasConversion(d => JsonConvert.SerializeObject(d),
                    d => JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(d));
            
            var department = new Department
            {
                DepartmentId = 1,
                FacultyId = 1,
                Name = "Кафедра Информационной Безопасности"
            };

            var faculty = new Faculty
            {
                FacultyId = 1,
                OrganizationId = 1,
                Name = "Факультет Информационных Технологий"
            };

            modelBuilder.Entity<Department>()
                .HasData(department);

            modelBuilder.Entity<Faculty>()
                .HasData(faculty);

            modelBuilder.Entity<Organization>()
                .HasData(new Organization
                {
                    OrganizationId = 1,
                    Name = "Московский Политехнический Университет"
                });

            modelBuilder.Entity<Position>()
                .HasData(new List<Position>
                {
                    new()
                    {
                        PositionId = 1,
                        Name = "доцент"
                    },
                    new()
                    {
                        PositionId = 2,
                        Name = "студент"
                    }
                });
        }
    }
}