using System.Collections.Generic;
using System.Threading.Tasks;
using SPNR.Core.Models;

namespace SPNR.Core.Services.Selection.Drivers
{
    [SelectionDriver("scopus")]
    public class Scopus : ISelectionDriver
    {
        public Task<List<ScientificWork>> Search(SearchInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}