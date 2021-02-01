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

        private async Task ListAuthorsHandler(string org, string fac, string dep)
        {
            _logger.Warning($"{org} : {fac} : {dep}");
        }
    }
}