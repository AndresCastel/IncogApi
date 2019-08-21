using System.Collections.Generic;
using IncognitusBack.API.Interfaces;
using IncognitusBack.API.Services;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Services;
using IncognitusBack.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IncognitusBack.API
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
            services.AddMvcCore();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<IncogDbContext>(c =>
             c.UseSqlServer(Configuration.GetConnectionString("IncogServerConnection"), b => b.MigrationsAssembly("IncognitusBack.API")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "IncogitusBack.API", Version = "v1" });
                c.OperationFilter<MyHeaderFilter>();
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IEmployeeViewModelService, EmployeeViewModelService>();
            services.AddScoped<IChargesViewModelService, ChargesViewModelService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IncogitusBack.API V1");

            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
    public class MyHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = false // set to false if this is optional
            });
        }
    }
}
