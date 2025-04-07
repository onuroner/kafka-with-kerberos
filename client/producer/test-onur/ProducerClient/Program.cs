
using Confluent.Kafka;
using ProducerClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ProducerClient;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Start producing events ...");

        var serviceProvider = new ServiceCollection().AddSingleton<IProducerService, ProducerService>()
            .AddLogging(configure => configure.AddConsole())
            .AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
            .BuildServiceProvider();

        var producerService = serviceProvider.GetRequiredService<IProducerService>();
        var cts = new CancellationTokenSource();
        producerService.StartAsync(cts.Token);
    }
}

