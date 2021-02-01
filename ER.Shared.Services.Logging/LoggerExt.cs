using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ER.Shared.Services.Logging
{
    public static class LoggerExt
    {
        public static void AddSerilogFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
        }

        public static void DumpException(this ILogger logger, Exception exception)
        {
            static string BuildStringForException(Exception exc)
            {
                return ($"[{exc.GetType().FullName}] - " +
                        $"{(!string.IsNullOrWhiteSpace(exc.Message) ? exc.Message : "Empty message")}\n" +
                        $"{exc.StackTrace}\n").Trim();
            }

            logger.Error(BuildStringForException(exception));

            var innerException = exception.InnerException;

            while (innerException != null)
            {
                logger.Error($"With internal exception: {BuildStringForException(innerException)}");
                innerException = innerException.InnerException;
            }
        }
    }
}