using System.Linq;
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
        
        public void AddWork()
        {
            _dbContext.Add(new ScientificWork
            {
                AuthorName = "Kulev Egor"
            });

            _dbContext.SaveChanges();
        }

        public int WorkCount()
        {
            return _dbContext.Works.Count();
        }
    }
}