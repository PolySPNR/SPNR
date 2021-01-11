using System;
using Microsoft.Extensions.DependencyInjection;

namespace ER.Shared.Container
{
    public interface IContainerStartup
    {
        void RegisterServices(IServiceCollection serviceCollection);
        void ConfigureServices(IServiceProvider serviceProvider);
    }
}