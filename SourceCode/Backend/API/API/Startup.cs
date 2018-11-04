using System;
using System.IO;
using System.Reflection;
using API.Controllers;
using API.Core.BusinessLayer;
using API.Core.DataLayer;
using API.Core.DataLayer.Contracts;
using API.Core.DataLayer.Repositories;
using API.PolicyRequirements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace API
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
            // Add configuration for DbContext
            // Use connection string from appsettings.json file
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration["AppSettings:ConnectionString"]);
            });

            // Set up dependency injection for controller's logger
            services.AddScoped<ILogger, Logger<WarehouseController>>();
            services.AddScoped<ILogger, Logger<SalesController>>();

            // Set up dependency injection for repository
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ISalesService, SalesService>();

            services.AddMvc();

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("CustomerPolicy", policy => policy.Requirements.Add(new CustomerPolicyRequirement()));
                    options.AddPolicy("AdministratorPolicy", policy => policy.Requirements.Add(new AdministratorPolicyRequirement()));
                })
                .AddJsonFormatters();

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://localhost:5600";
                options.RequireHttpsMetadata = false;
                options.ApiName = "SnacksApi";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Snacks API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // todo: Set port number for client app
            app.UseCors(policy =>
            {
                // Add client origin in CORS policy
                policy.WithOrigins("http://localhost:4200");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snacks API");
            });

            app.UseMvc();
        }
    }
}
