using System.Diagnostics;
using ER.Shared.Services.Logging;
using Serilog;

namespace SPNR.Core.Services.Python
{
    public class PythonUnixService : IPythonService
    {
        private readonly ILogger _logger;

        public PythonUnixService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("Python");
        }
        
        public void Initialize()
        {
            _logger.Information("Using Unix Python service");
        }

        public string Exec(string script, string arguments)
        {
            using var scriptProcess = new Process
            {
                StartInfo =
                {
                    FileName = "python",
                    Arguments = $"\"{script}\" {arguments}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };
            scriptProcess.Start();

            return scriptProcess.StandardOutput.ReadToEnd();
        }
    }
}