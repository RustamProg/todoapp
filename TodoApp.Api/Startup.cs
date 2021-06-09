using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TodoApp.Api.Helpers;
using TodoApp.Api.Models.DbContexts;
using TodoApp.Api.Services;
using TodoApp.Api.Services.Repository;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.ServicesImplementations;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api
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
            services.AddDbContext<SqlServerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TodoConnection")));
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ApiName = "TodoAPI";
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                });
            services.AddSwaggerAuthentication();

            services.AddScoped<IDbRepository, DbRepository>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ITodoCommentsService, TodoCommentsService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            
            services.AddCors(options => {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("default");
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseMiddleware<CurrentUserMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}