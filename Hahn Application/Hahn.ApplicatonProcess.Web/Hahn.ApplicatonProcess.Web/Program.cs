using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.Data;
using Hahn.ApplicatonProcess.Data.models;
using Hahn.ApplicatonProcess.Data.Repository;
using Hahn.ApplicatonProcess.Domain.services;
using Hahn.ApplicatonProcess.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace Hahn.ApplicatonProcess.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false)
          .AddEnvironmentVariables()
          .Build();

            var filename = configuration.GetValue<string>("filename");

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(filename+".txt", rollingInterval: RollingInterval.Hour)
                        .CreateLogger();

            
            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseStartup<Program>()
                        .ConfigureLogging(logging =>
                                logging.AddFilter("System", LogLevel.Error)
                                .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Information)
                          )
                        .Build();
            host.Run();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            root.injectDependencies(services);
            services.AddCors();
            //services.AddCors(o => o.AddPolicy("HahnAPIPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
                
            //}));
            services.AddControllers();
            services.AddMvc(op =>
            {
                op.Filters.Add(new ValidatorActionFilter());
            }).AddFluentValidation(op =>
            {
                op.RegisterValidatorsFromAssemblyContaining<Program>();
            });
           
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IRepository<Applicant>, AppRepository<Applicant>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hahn Applicant API",
                    Description = "A sample ASP.NET Core Web API"
                }
                 );
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(
                options => options.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader()
              );
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseCors("HahnAPIPolicy");
           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
