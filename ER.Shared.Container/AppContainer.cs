using System;
using Microsoft.Extensions.DependencyInjection;

namespace ER.Shared.Container
{
    public class AppContainer<TStartup, TContainerEntryPoint>
        where TStartup : class, IContainerStartup, new()
        where TContainerEntryPoint : class, IContainerEntryPoint
    {
        private IServiceProvider _serviceProvider;
        
        public void Initialize()
        {
            var container = new ServiceCollection();
            var startupInstance = new TStartup();

            container.AddSingleton<IContainerEntryPoint, TContainerEntryPoint>();
            startupInstance.RegisterServices(container);

            _serviceProvider = container.BuildServiceProvider();
            startupInstance.ConfigureServices(_serviceProvider);
        }
        
        public void Run()
        {
            var entryPoint = _serviceProvider.GetService<IContainerEntryPoint>();
            entryPoint.Run();
        }
    }
}