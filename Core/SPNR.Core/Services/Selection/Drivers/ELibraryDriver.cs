using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ER.Shared.Services.Logging;
using Newtonsoft.Json;
using Serilog;
using SPNR.Core.Api.ELibrary;
using SPNR.Core.Api.ELibrary.Credentials;
using SPNR.Core.Misc;
using SPNR.Core.Models;
using SPNR.Core.Models.Works;

namespace SPNR.Core.Services.Selection.Drivers
{
    [SelectionDriver("elib")]
    public class ELibraryDriver : ISelectionDriver
    {
        private readonly ELibApi _api = new();
        private ILogger _logger;

        public ELibraryDriver(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("ELIB");
        }
        
        public async Task Initialize()
        {
            _logger.Verbose("Initialising");
            var authType = new EnvVar<ELibCredentialsType>("SPNR_ELIB_AUTH_TYPE", ELibCredentialsType.LogPassPair);
            var authCred1 = new EnvVar<string>("SPNR_ELIB_AUTH_CRED1", null);
            var authCred2 = new EnvVar<string>("SPNR_ELIB_AUTH_CRED2", null);
            
            _logger.Verbose($"Auth type: {authType.Value}");

            if (authCred1.Value != null && authCred2.Value != null)
            {
                var auth = _api.Authorize(authType.Value, authCred1.Value, authCred2.Value);
                _logger.Verbose(JsonConvert.SerializeObject(auth));
            }
            
            _logger.Verbose("Initialised");
        }

        public async Task<List<ScientificWork>> Search(SearchInfo info)
        {
            _logger.Verbose("Searching");
            var pageId = 1;
            var works = new List<ScientificWork>();

            var idList = await _api.GetWorkIds(info, pageId);

            foreach (var id in idList.Data.Take(10))
            {
                _logger.Verbose($"Getting info for id: {id}");
                var workAnswer = await _api.GetWorkInfo(id);
                works.Add(workAnswer.Data);

                SpinWait.SpinUntil(() => _api.CheckCooldown());
            }

            return works;
        }
    }
}