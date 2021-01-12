using System;
using System.Linq;
using System.Threading.Tasks;
using ER.Shared.Services.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SPNR.Core.Models;
using SPNR.Core.Services.Data.Contexts;

namespace SPNR.Core.Services.Data
{
    public class DataService
    {
        private readonly ScWorkContext _dbContext;
        private readonly ILogger _logger;

        public DataService(ILoggerFactory loggerFactory, ScWorkContext dbContext)
        {
            _logger = loggerFactory.GetLogger("Data");
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _logger.Verbose("Running migrations");
            _dbContext.Database.Migrate();
            _logger.Verbose("Service initialized");
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
    }
}