using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ER.Shared.Services.Logging;
using Newtonsoft.Json;
using Scopus.Api.Client;
using Scopus.Api.Client.Models.Common;
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
            
            
            // if (apiKey.Value == null)
            // {
            //     _logger.Warning("API Key is not set. Driver won't be used");
            //     return;
            // }

            var api = new ScopusSearchClient("https://api.elsevier.com/", "8d51321ac59b87444e3c31d5487e8542");
            
            var scopusSearchResult = await api.GetAsync<SearchResults<Scopus.Api.Client.Models.Search.Scopus>>("content/search/scopus", "query=AUTHOR-NAME(budylina e)");

            foreach (var scopus in scopusSearchResult.Data.Entry)
            {
                _logger.Information($"{scopus.PrismPublicationName}");
            }
            
            File.WriteAllText("./result.json", JsonConvert.SerializeObject(scopusSearchResult, Formatting.Indented));
        }

        public Task<List<ScientificWork>> Search(SearchInfo info)
        {
            return null;
        }
    }
}