using DocumentManagementSystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DocumentManagementSystem.Core.UnitOfWork;
using DocumentManagementSystem.Data.UnitOfWorks;
using DocumentManagementSystem.Core.Repositories;
using DocumentManagementSystem.Data.Repositories;
using DocumentManagementSystem.Service.Services;
using DocumentManagementSystem.Core.Services;
using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Entities;

namespace DocumentManagementSystem.Web
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

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<IMailer, Mailer>();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));           
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();//Session için
            services.AddTransient<DmsDbContext>();
            services.AddDbContextPool<DmsDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),  o =>
               {
                   o.MigrationsAssembly("DocumentManagementSystem.Data");
               }
            ));
           
            services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
             });
            services.AddControllersWithViews();
            services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(15));//Session Süresi
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

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

            app.UseSession();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
