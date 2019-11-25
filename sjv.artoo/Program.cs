using System;
using System.IO;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;
using Topshelf;

namespace sjv.artoo
{
    public class Program
    {
        public static IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.RollingFile(new JsonFormatter(),
                    $"{Configuration["LogPath"]}{Configuration["ServiceName"]}.txt")
                .CreateLogger();

            HostFactory.Run(cfg =>
            {
                cfg.Service<SjvArtoo>(x =>
                {
                    x.ConstructUsing(name =>
                    {
                        IServiceProvider container = null;
                        var services = new ServiceCollection();
                        services.AddSingleton(Log.Logger);

                        services.AddSingleton(context => Bus.Factory.CreateUsingRabbitMq(bus2cfg =>
                        {
                            var host = bus2cfg.Host(new Uri(Configuration["RabbitMq:amqpUri"]), h => { });
                        }));

                        //var rabbitMqOptions = new RabbitMqOptions();
                        //Configuration.GetSection("RabbitMq").Bind(rabbitMqOptions);
                        //services.AddSingleton(rabbitMqOptions);

                        //services.AddMassTransit(x =>
                        //{
                        //    mt.AddConsumer<consumerType>();
                        //    mt.AddWhateverConsumers();
                        //});


                        services.AddSingleton<SjvArtoo>();
                        container = services.BuildServiceProvider();
                        var service = container.GetService<SjvArtoo>();
                        return service;
                    });
                    x.WhenStarted((s, hostControl) => s.Start(hostControl));
                    x.WhenStopped((s, hostControl) => s.Stop(hostControl));
                });
            });
        }
    }
}