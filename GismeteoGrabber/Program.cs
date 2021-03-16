using System;
using System.Configuration;
using System.IO;
using CommonProject.Repositories;
using CommonProject.Repositories.Interfaces;
using GismeteoGrabber.Scheduler;
using GismeteoGrabber.Utilities;
using GismeteoGrabber.Utilities.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GismeteoGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            host.RunAsync();
           
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    ParserScheduler.Start(serviceProvider);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            Console.ReadKey();
        }


        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => ConfigurateServices(services));

        static void ConfigurateServices(IServiceCollection services)
        {
            var configuration = GetConfigurationRoot();
            services.AddSingleton(GetConfigurationRoot())
                    .AddSingleton<IRequestHandler, HtmlWebRequestHandler>()
                    .AddSingleton<IWeatherRepository>(new WeatherRepository(configuration.GetConnectionString("MySqlConnection")))
                    .AddSingleton<IParser, GismeteoParser>()
                    .AddTransient<JobFactory>()
                    .AddScoped<ParserJob>();
        }


        static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
               .AddJsonFile("appsettings.json", false)
               .Build();
        }
    }    
}
