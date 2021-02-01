﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ER.Shared.Services.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using SPNR.Core.Models;
using SPNR.Core.Models.AuthorInfo;
using SPNR.Core.Models.Works;
using SPNR.Core.Services.Data.Contexts;
using SPNR.Core.Services.Data.Models;
using SPNR.Core.Services.Python;

namespace SPNR.Core.Services.Data
{
    public class DataService
    {
        private readonly ScWorkContext _dbContext;
        private readonly PythonService _pythonService;
        private readonly ILogger _logger;

        public DataService(ILoggerFactory loggerFactory, ScWorkContext dbContext, PythonService pythonService)
        {
            _logger = loggerFactory.GetLogger("Data");
            _dbContext = dbContext;
            _pythonService = pythonService;
        }

        public void Initialize()
        {
            _logger.Verbose("Running migrations");
            _dbContext.Database.Migrate();
            _logger.Verbose("Service initialized");
        }

        public async Task<List<Organization>> GetOrganizations()
        {
            return await _dbContext.Organizations.ToListAsync();
        }

        public async void AddWork(ScientificWork scientificWork)
        {
            await _dbContext.AddAsync(scientificWork);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> WorkCount()
        {
            return await _dbContext.Works.CountAsync();
        }

        // TODO: Rewrite for async operations
        public async Task<ScientificWork> ListWorks(SearchInfo searchInfo)
        {
            return null;
        }

        public async Task ImportAuthorsFromFile(string file)
        {
            var json = _pythonService.Exec(Path.GetFullPath(Path.Combine(
                    AppContext.BaseDirectory,
                    "scripts",
                    "import.py")),
                file);
            
            var list = JsonConvert.DeserializeObject<List<AuthorImport>>(json);
            
            foreach (var author in list)
            {
                var org = _dbContext.Organizations.FirstOrDefault(o => o.Name == author.Organization) ??
                          new Organization
                          {
                              Name = author.Organization
                          };

                var fac = _dbContext.Faculties.FirstOrDefault(f => f.Name == author.Faculty) ??
                          new Faculty
                          {
                              Name = author.Faculty,
                              Organization = org
                          };

                var dep = _dbContext.Departments.FirstOrDefault(d => d.Name == author.Department) ??
                          new Department
                          {
                              Name = author.Department,
                              Faculty = fac
                          };

                var pos = _dbContext.Positions.FirstOrDefault(p => p.Name == author.Position) ??
                          new Position
                          {
                              Name = author.Position
                          };

                var nAuthor = _dbContext.Authors.FirstOrDefault(a => a.Name == author.Name);

                if (nAuthor == null)
                {
                    await _dbContext.AddAsync(new Author
                    {
                        Name = author.Name,
                        Organization = org,
                        Faculty = fac,
                        Department = dep,
                        Position = pos
                    });
                }
                
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}