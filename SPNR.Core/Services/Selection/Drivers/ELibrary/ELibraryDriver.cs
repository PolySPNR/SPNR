using System.Threading.Tasks;
using SPNR.Core.Models;

namespace SPNR.Core.Services.Selection.Drivers.ELibrary
{
    [SelectionDriver("elib")]
    public class ELibraryDriver : ISelectionDriver
    {
        public async Task<string> Search(SearchInfo info)
        {
            return null;
        }
    }
}