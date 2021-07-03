using System;
using Microsoft.Extensions.DependencyInjection;
using SPNR.Core.Services.Data;
using SPNR.Core.Services.Data.Contexts;
using SPNR.Core.Services.Python;
using SPNR.Core.Services.Selection;

namespace SPNR.Core.Services
{
    public static class CoreServicesExt
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection collection)
        {
            collection.AddSingleton<ScWorkContext>();
            collection.AddSingleton<DataService>();
            collection.AddSingleton<SelectionService>();
            
            if(Environment.OSVersion.Platform == PlatformID.Win32NT)
                collection.AddSingleton<IPythonService, PythonWindowsService>();
            else
                collection.AddSingleton<IPythonService, PythonUnixService>();

            return collection;
        }
    }
}