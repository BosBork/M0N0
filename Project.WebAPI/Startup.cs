using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using AutoMapper.Extensions.ExpressionMapping;
using Project.Common;
using Project.DAL;
using Project.Repository.Common.Interfaces.UOW;
using Project.Service.Common;
using Project.Repository.Repo.UOW;
using Project.Service;
using Project.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Project.Common.GlobalError;
using Project.Common.Error;

namespace Project.WebAPI
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
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mono Vehicles",
                    Version = "v1",
                });
            });

            #region Sorting
            services.AddScoped<ISortHelper<VehicleMake>, SortHelper<VehicleMake>>();
            services.AddScoped<ISortHelper<VehicleModel>, SortHelper<VehicleModel>>();
            #endregion

            #region Wrappers
            services.AddScoped<IRepoWrapper, RepoWrapper>();
            services.AddScoped<IServicesWrapper, ServicesWrapper>();
            #endregion

            services.AddAutoMapper(cfg => cfg.AddExpressionMapping(), typeof(Startup));

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mono Vehicles v1");
                    #region HideModelsSchemeFromUI
                    c.DefaultModelExpandDepth(0);
                    c.DefaultModelsExpandDepth(-1); 
                    #endregion
                    #region extra
                    //c.RoutePrefix = string.Empty; //launchUrl in launchSettings.json must be empty for the swagger root page to load correctly 
                    #endregion
                });

            #region GlobalErrorHandling
            app.ConfigGlobalExceptionHandler();
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
