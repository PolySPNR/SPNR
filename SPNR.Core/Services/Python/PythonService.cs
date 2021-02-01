using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using ER.Shared.Services.Logging;
using Serilog;

namespace SPNR.Core.Services.Python
{
    // man, this code sucks
    public class PythonService
    {
        private readonly ILogger _logger;

        public PythonService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("Python");
        }

        public void Initialize()
        {
            if (!File.Exists("./python/win/python.exe")) Download();

            if (!File.Exists("./python/win/Scripts/pip.exe")) SetUp();

            using var pipList = new Process
            {
                StartInfo =
                {
                    FileName = "./python/win/Scripts/pip.exe",
                    Arguments = "list",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };
            pipList.Start();

            var output = pipList.StandardOutput.ReadToEnd();

            if (output.Contains("docx") && output.Contains("python-docx") && output.Contains("jsons"))
                return;

            GetDependencies();
        }

        private void Download()
        {
            _logger.Information("Downloading Python distribution");
            string link;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                link = "https://www.python.org/ftp/python/3.7.9/python-3.7.9-embed-win32.zip";
            else
                return;

            Directory.CreateDirectory("./python/win");

            var wc = new WebClient();
            wc.DownloadFile(link, "./python.zip");

            _logger.Information("Extracting Python distribution");
            ZipFile.ExtractToDirectory("./python.zip", "./python/win");

            File.Delete("./python.zip");
        }

        private void SetUp()
        {
            _logger.Information("Setting up");
            var pth = "./python/win/python37._pth";
            File.WriteAllText(pth, File.ReadAllText(pth).Replace("#import site", "import site"));

            _logger.Information("Setting up pip");

            _logger.Information("Downloading get-pip.py");
            var wc = new WebClient();
            wc.DownloadFile("https://bootstrap.pypa.io/get-pip.py", "./python/win/get-pip.py");

            _logger.Information("Installing pip");
            Process.Start("./python/win/python.exe", "./python/win/get-pip.py")?.WaitForExit();
        }

        private void GetDependencies()
        {
            _logger.Information("Installing dependencies");
            Process.Start("./python/win/Scripts/pip.exe", "install docx python-docx jsons")?.WaitForExit();
        }

        public string Exec(string script, string arguments)
        {
            using var scriptProcess = new Process
            {
                StartInfo =
                {
                    FileName = "./python/win/python.exe",
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