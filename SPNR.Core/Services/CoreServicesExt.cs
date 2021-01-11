﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPNR.Core.Services.Data;
using SPNR.Core.Services.Data.Contexts;
using SPNR.Core.Services.Selection;

namespace SPNR.Core.Services
{
    public static class CoreServicesExt
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection collection)
        {
            collection.AddDbContext<ScWorkContext>();
            collection.AddSingleton<DataService>();
            collection.AddSingleton<SelectionService>();
            
            return collection;
        }
    }
}