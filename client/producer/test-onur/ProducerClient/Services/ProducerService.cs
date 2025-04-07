
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerClient.Services;
public class ProducerService : IProducerService
{
    private readonly ILogger<ProducerService> _logger;
    private readonly IConfiguration _configuration;
    private IProducer<Null, string> _producer;
    public ProducerService(ILogger<ProducerService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        var config = new ProducerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"],

            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Gssapi,
            SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
            SaslKerberosServiceName = "kafka",
            SaslKerberosPrincipal = "producer-client/producer-client@KAFKA.SECURE",
            SaslKerberosKinitCmd= "kinit -k -t /mnt/keytabs/producer-client-client.keytab producer-client/producer-client@KAFKA.SECURE -V",
            SaslKerberosKeytab = "/mnt/keytabs/producer-client-client.keytab",
            SslCaLocation = "/mnt/kafka/secrets/ca-cert",
            Debug = "security,broker,protocol"

        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        string text = null;
    
        for (int i = 0; i < 5; i++)
        {
            text = $"Hello for {i} times!";
            var message = new Message<Null, string>
            {
                Value = text
            };

            await _producer.ProduceAsync("kafka_demo_topic", message, cancellationToken);
            Console.WriteLine($"Produced message: {text}");
        }
        await StopAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
        return Task.CompletedTask;
    }
}
