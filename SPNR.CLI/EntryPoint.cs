using ER.Shared.Container;
using ER.Shared.Services.Logging;
using Serilog;
using SPNR.Core.Services.Data;
using SPNR.Core.Services.Data.Contexts;
using SPNR.Core.Services.Python;
using SPNR.Core.Services.Selection;

namespace SPNR.CLI
{
    public partial class EntryPoint : IContainerEntryPoint
    {
        private readonly DataService _dataService;
        private readonly ILogger _logger;
        private readonly PythonService _pythonService;
        private readonly SelectionService _selectionService;

        public EntryPoint(ILoggerFactory loggerFactory, DataService dataService, SelectionService selectionService,
            PythonService pythonService)
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