using System;
using System.IO;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Demo.Bll.Classes;
using Demo.Bll.Interfaces;
using Demo.BLL.Classes;
using Demo.BLL.Helpers;
using Demo.BLL.Models;
using Demo.BLL.Validation;

namespace Demo
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            IConfiguration configuration = builder.Build();

            services.AddSingleton(configuration);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddSerilog();
            });

            services.AddSingleton<IJsonDirectory>(s => new JsonDirectory(GetBasePath(), "data.json"));
            services.AddScoped<IValidator<StandardPersonData>, StandardRowValidator>();
            services.AddScoped<IFileWrapper, FileWrapper>();
            services.AddScoped<IStandardJsonProcessor, StandardJsonProcessor>();
            services.AddScoped<IMigrationStrategy, StandardMigrationStrategy>();
            services.AddScoped<IDataMigration, DataMigration>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddSingleton<EntryPoint>();

            return services;
        }

        private static string GetBasePath()
        {
            var workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        }
    }
}