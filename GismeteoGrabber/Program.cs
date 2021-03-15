using System;
using System.Threading.Tasks;
using CommonProject.Repositories;
using CommonProject.Repositories.Interfaces;
using GismeteoGrabber.Scheduler;
using GismeteoGrabber.Utilities;
using GismeteoGrabber.Utilities.Interfaces;
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
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IRequestHandler, HtmlWebRequestHandler>()
                    .AddSingleton<IWeatherRepository>(new WeatherRepository("server=localhost;port=3306;user=root;password=1234;database=weatherDb;"))
                    .AddSingleton<IParser, GismeteoParser>()
                    .AddTransient<JobFactory>()
                    .AddScoped<ParserJob>()
                );
        }    
}
