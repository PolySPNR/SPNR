using System.Collections.Generic;
using System.Threading.Tasks;
using SPNR.Core.Models;

namespace SPNR.Core.Services.Selection.Drivers.ELibrary
{
    [SelectionDriver("elib")]
    public class ELibraryDriver : ISelectionDriver
    {
        public async Task<List<ScientificWork>> Search(SearchInfo info)
        {
            return new List<ScientificWork>();
        }
    }
}