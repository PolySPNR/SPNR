using System;
using ER.Shared.Container;
using ER.Shared.Services.Logging;
using Microsoft.Extensions.DependencyInjection;
using SPNR.Core.Services;

namespace SPNR.CLI
{
    public class Startup : IContainerStartup
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSerilogFactory();
            serviceCollection.AddCoreServices();
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
        }
    }
}