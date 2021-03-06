﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeduCoreApp.Data;
using TeduCoreApp.Models;
using TeduCoreApp.Services;
using TeduCoreApp.EF;
using TeduCoreApp.Data.Entities;
using AutoMapper;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.EF.Repositories;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.Implementation;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using TeduCoreApp.Helpers;
using TeduCoreApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TeduCoreApp.Authorization;





//using AppUser = TeduCoreApp.Data.Entities.AppUser;


namespace TeduCoreApp
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),o=>o.MigrationsAssembly("TeduCoreApp.EF")));
            services.AddAutoMapper();
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            //services.AddSingleton(Mapper.Configuration);
            // cofig identity 
            services.Configure<IdentityOptions>(options =>
            {
                //  Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false; //có viết thưởng hay không
                options.Password.RequireNonAlphanumeric = false; // có số hay không
                options.Password.RequireUppercase = false;// có kí tự viết hoa không
                options.Password.RequiredLength = 6;// chiều dai là bao nhiêu
                options.Password.RequiredUniqueChars = 0;// có kí tự đặc biệt hay không

                // Lock setting
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                // user seeting
                options.User.RequireUniqueEmail = true;
            });
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
            services.AddTransient(typeof(IUnitofWork), typeof(EFUnitofWork));
            //Repositrory
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IFunctionRepository, FuctionReponsitory>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductTagRepository, ProductTagRepository>();
            services.AddTransient<IPermissionRepository, PermissionReponsitory>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            //Service
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ISizeService, SizeService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationCrudHandler>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<DbIntializer>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile("Logs/tedu-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
                    );
              
            });
           
        }
    }
}
