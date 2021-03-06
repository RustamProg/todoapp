using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TodoApp.Identity.Models;
using TodoApp.Identity.Services;

namespace TodoApp.Identity
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
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TodoApp.Identity", Version = "v1"});
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                //.AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>();
                .AddDefaultTokenProviders(); // Что это делает?

            services.AddIdentityServer(options => options.IssuerUri = "localhost")
                .AddInMemoryApiResources(IdentityConfiguration.GetApiResources())
                .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityConfiguration.GetScopes())
                .AddInMemoryClients(IdentityConfiguration.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential(false);

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "631863314589-78e0flbpm57l2rg6gi6h7meunj68f4in.apps.googleusercontent.com";
                    options.ClientSecret = "LfOYActB2UPuPmW9sho7c_zi";
                });
            
            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();
            
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp.Identity v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("default");

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}