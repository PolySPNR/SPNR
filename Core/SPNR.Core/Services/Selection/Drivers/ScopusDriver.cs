using System.Collections.Generic;
using System.Threading.Tasks;
using ER.Shared.Services.Logging;
using Serilog;
using SPNR.Core.Api.Scopus;
using SPNR.Core.Misc;
using SPNR.Core.Models;
using SPNR.Core.Models.Works;

namespace SPNR.Core.Services.Selection.Drivers
{
    [SelectionDriver("scopus")]
    public class ScopusDriver : ISelectionDriver
    {
        private readonly ILogger _logger;
        private ScopusApi _api;

        public ScopusDriver(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("SCOPUS");
        }

        public async Task Initialize()
        {
            var apiKey = new EnvVar<string>("SPNR_SCOPUS_API_KEY", null);

            if (apiKey.Value == null)
            {
                _logger.Warning("API Key is not set. Driver won't be used");
                return;
            }
            
            // TODO register API
        }

        public Task<List<ScientificWork>> Search(SearchInfo info)
        {
            return null;
        }
    }
}