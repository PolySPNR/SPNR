using System.IO;
using System.Threading.Tasks;

namespace SPNR.CLI
{
    public partial class EntryPoint
    {
        private async Task ImportAuthorsHandler(FileInfo file)
        {
            await _dataService.ImportAuthorsFromFile(file.FullName);
        }
    }
}