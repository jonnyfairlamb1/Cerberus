using DatabaseAccessService.Domain.Extensions;
using DatabaseAccessService.Extensions;
using DatabaseAccessService.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Prometheus;
using System.Reflection;

internal class Program {

    private static void Main(string[] args) {
        //get the project name
        string applicationName = AppDomain.CurrentDomain.FriendlyName;

        WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions {
            ApplicationName = applicationName,
            Args = args,
            ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
        });

        //configure as windows service
        builder.Host.UseWindowsService();

        //configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddNLog("NLog.config.xml");

        //add controllers and format json
        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
            options.SuppressMapClientErrors = true;
        }).AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.Formatting = Formatting.Indented;
        });

        //Add swagger docs
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo {
                Version = "v1",
                Title = "Database Access Service API",
                Description = "An ASP.NET Core Web API that allows users to retrieve information from database."
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });

        //add memory cache
        builder.Services.AddMemoryCache();

        //add services to the container
        builder.Services.AddInfrastructureServices();
        builder.Services.AddDomainServices();
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        //configure http request pipepline
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        //prometheus
        app.UseHttpMetrics();
        app.UseRouting();
        app.UseEndpoints(endpoints => {
            endpoints.MapMetrics();
        });
        app.Run();
    }
}