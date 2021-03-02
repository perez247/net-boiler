using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Filters;
using Application.Infrastructure.AutoMapper;
using Application.Infrastructure.RequestResponsePipeline;
using FluentValidation.AspNetCore;
using Infrastructure.Extensions;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence.Extensions;

namespace Api
{
    /// <summary>
    /// Start up class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// The I configuration
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// I hosting 
        /// </summary>
        private readonly IWebHostEnvironment _env;

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

            //  Configure Database and Microsoft Identity
            services.ConfigureDatabaseConnections(
                connectionString,
                "Api",
                _env.IsStaging()
            );

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(WebCustomExceptionFilter));
            })
            .AddNewtonsoftJson();
            // .AddFluentValidation((fv => fv.RegisterValidatorsFromAssemblyContaining<SignUpValidator>()));

            // Handle Model state errors
            services.AddScoped<IValidatorInterceptor, ValidatorInterceptor>();

            // Add DataContext implementation of Application interfaces
            services.ImplementApplicationDatabaseInterfaces();

            // Add Infrastructure implementation of Application interfaces
            services.AddInfractureServices();

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            //Add Mediator
            services.AddMediatR(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Use SignalR
            services.AddSignalR();

            // Check for JWT authentication where neccessary
            services.AddJwtAuthentication();

            services.AddMemoryCache(); 

            services.AddSwaggerDocumentation();

            // Add Swagger Open API
            if (!_env.IsProduction())
            {
                // Allow cors 
                services.AddCors(options =>
                    options.AddPolicy("MyPolicy", builder =>
                    {
                        builder
                            // .WithOrigins("http://localhost:4200")
                            .SetIsOriginAllowed(_ => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }));
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("MyPolicy");
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerDocumentation();
            }

            app.EnsureDatabaseAndMigrationsExtension();
            // Sedd the database with dummy database and real data
            
            app.SeedDatabase().Wait();
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
