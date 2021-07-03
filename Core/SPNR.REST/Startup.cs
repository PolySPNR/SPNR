using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ER.Shared.Services.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SPNR.Core.Services;
using SPNR.Core.Services.Data;
using SPNR.Core.Services.Python;
using SPNR.Core.Services.Selection;

namespace SPNR.REST
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SPNR.REST", Version = "v1"});
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SPNR.Core.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SPNR.REST.xml"));
            });
            services.AddSerilogFactory();
            services.AddCoreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPNR.REST v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.ApplicationServices.GetService<DataService>()?.Initialize();
            app.ApplicationServices.GetService<IPythonService>()?.Initialize();
            app.ApplicationServices.GetService<SelectionService>()?.Initialize();
        }
    }
}