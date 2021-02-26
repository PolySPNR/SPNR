using System;
using Serilog;

namespace ER.Shared.Services.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name, string format = null)
        {
            var logger = new LoggerConfiguration();
            format ??= $"[{{Timestamp:HH:mm:ss}}] [{{Level:u3}}] [{name}] {{Message:lj}}{{NewLine}}";

#if DEBUG
            logger.MinimumLevel.Verbose();
#else
            logger.MinimumLevel.Information();
#endif

            bool.TryParse(Environment.GetEnvironmentVariable("ERX_FILE_LOG") ?? "false", out var fileLog);

            logger
                .WriteTo
                .Console(outputTemplate: format);

            if (fileLog)
                logger.WriteTo.File($"logs/{DateTime.Now:yyyy-MM-dd_HH-mm-ss}/erx.log", outputTemplate: format,
                    shared: true, rollOnFileSizeLimit: true);

            var l = logger.CreateLogger();

            return l;
        }
    }
}