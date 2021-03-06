using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SPNR.Core.Models.AuthorInfo;
using SPNR.Core.Services.Data.Contexts;
using Xunit;

namespace Tests
{
    public class DatabaseTests
    {
        private readonly ScWorkContext _context;

        private static readonly Organization TestOrg = new Organization
        {
            OrganizationId = 2,
            Name = "Test Organization"
        };

        public DatabaseTests()
        {
            File.Delete("./spnr.db");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ScWorkContext>()
                .UseSqlite("Data Source=./spnr.db");

            _context = new ScWorkContext(dbContextOptionsBuilder.Options);
            _context.Database.Migrate();
        }

        /// <summary>
        /// Maintained by Nikita
        /// </summary>
        [Fact]
        public void MigrationsOk()
        {
            Assert.NotEmpty(_context.Database.GetMigrations());
        }

        /// <summary>
        /// Maintained by Vladislav
        /// </summary>
        [Fact]
        public void CanConnect()
        {
            Assert.True(_context.Database.CanConnect());
        }

        /// <summary>
        /// Maintained by Valeria
        /// </summary>
        [Fact]
        public void AddOk()
        {
            _context.Organizations.Add(TestOrg);

            Assert.NotEqual(0, _context.SaveChanges());
            Assert.True(_context.Organizations.Any(o => o.Name == "Test Organization"));
        }
    }
} 
