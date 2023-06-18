using CapstoneProjectLibrary;
using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneProjectAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly IDbConfig _configuration = new DbConfiguration();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddScoped<IDbConfig, DbConfiguration>();

            services.AddScoped<IRepository, GameRepository>();

            services.AddScoped<ICommentRepo, CommentRepo>();

            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddScoped<IGenresRepository, GameGenresRepository>();

            services.AddDbContext<EntityContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Name = ".GameStoreCookie";
                   options.Cookie.SameSite = SameSiteMode.None;
                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                   options.Cookie.Path = "/";
                   options.Cookie.HttpOnly = false;
               });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://127.0.0.1:5173")
               .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiApp v1"));
            }

            app.UseRouting();
            app.UseCors("AllowOrigin");

            app.UseAuthentication();
            app.UseAuthorization();
 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
