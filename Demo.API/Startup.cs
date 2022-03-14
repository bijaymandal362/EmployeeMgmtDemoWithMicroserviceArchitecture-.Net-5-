using Demo.BusinessLayer.EmployeeHistory;
using Demo.BusinessLayer.Employees;
using Demo.BusinessLayer.Position;
using Demo.BusinessLayer.Validation;
using Demo.Entities.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API
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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    x =>
                    {
                        x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials();
                    });
            });
            services.AddMvc();
            services.AddControllers();
            services.AddControllersWithViews().AddNToastNotifyNoty(new NToastNotify.NotyOptions()
            {
                ProgressBar = true, //show the progress bar
                Timeout = 5000, //notification 5000ms ma disappear huncha
                Theme = "mint" //Notify.js theme name ho but for more theme goto Notify.js website

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo.API", Version = "v1" });
            });
            services.AddDbContextPool<DemoDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IPositionService, PostionService>();
            services.AddScoped<IEmployeeHistoryService, EmployeeHistoryService>();
            services.AddScoped<IValidateService, ValidateService>();
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo.API v1"));
            }

            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
