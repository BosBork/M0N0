using Project.DAL.DataAccess;
using Project.DAL;
using Project.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Repository.Common.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Repository.Repo.UOW;
using Project.Service;
using Project.Service.Common;
using Project.Repository.Common.Interfaces;
using Project.Repository.Repo;
using AutoMapper;
using Project.Model.Common;
using AutoMapper.Extensions.ExpressionMapping;

namespace Project.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            #region Sorting
            services.AddScoped<ISortHelper<VehicleMake>, SortHelper<VehicleMake>>();
            services.AddScoped<ISortHelper<VehicleModel>, SortHelper<VehicleModel>>();
            #endregion

            #region Wrappers
            services.AddScoped<IRepoWrapper, RepoWrapper>();
            services.AddScoped<IServicesWrapper, ServicesWrapper>(); 
            #endregion

            #region AMtest
            //services.AddAutoMapper(
            //    cfg => cfg.AddExpressionMapping().AddProfile(new MappingProfile())); 
            #endregion

            services.AddAutoMapper(cfg => cfg.AddExpressionMapping(), typeof(Startup));

            services.AddControllersWithViews()
                .AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, IRepoWrapper repoWrapper*/)
        {
            //string car1 = repoWrapper.VehicleMake.FindByCondition(x => x.VehicleMakeId.Equals(1)).FirstOrDefault().Name;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}