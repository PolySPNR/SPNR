using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using ER.Shared.Container;
using ER.Shared.Services.Logging;
using Serilog;
using SPNR.Core.Models;
using SPNR.Core.Models.AuthorInfo;
using SPNR.Core.Models.Works;
using SPNR.Core.Services.Data;
using SPNR.Core.Services.Data.Contexts;
using SPNR.Core.Services.Python;
using SPNR.Core.Services.Selection;

namespace SPNR.CLI
{
    public partial class EntryPoint : IContainerEntryPoint
    {
        private readonly DataService _dataService;
        private readonly SelectionService _selectionService;
        private readonly PythonService _pythonService;
        private readonly ScWorkContext _workContext;
        private readonly ILogger _logger;

        public EntryPoint(ILoggerFactory loggerFactory, DataService dataService, SelectionService selectionService, PythonService pythonService)
        {
            _logger = loggerFactory.GetLogger("CLI");
            _dataService = dataService;
            _selectionService = selectionService;
            _pythonService = pythonService;
        }

        public void Run()
        {
            _logger.Information("SPNR / SWSS (Scientific Works Search System)");
            _dataService.Initialize();
            _selectionService.Initialize();
            
            _pythonService.Initialize();

            BuildCommands();
        }
    }
}