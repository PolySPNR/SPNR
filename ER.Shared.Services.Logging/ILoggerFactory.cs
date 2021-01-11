using Serilog;

namespace ER.Shared.Services.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(string name, string format = null);
    }
}