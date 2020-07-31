using Confluent.Kafka;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HttpTracker.KafkaProducer.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: .. brokerList topicName");
                return;
            }

            var brokerList = args.First();
            var topicName = args.Last();

            var config = new ProducerConfig { BootstrapServers = brokerList };

            using var producer = new ProducerBuilder<string, string>(config).Build();

            Console.WriteLine("\n-----------------------------------------------------------------------");
            Console.WriteLine($"Producer {producer.Name} producing on topic {topicName}.");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("To create a kafka message with UTF-8 encoded key and value:");
            Console.WriteLine("> key value<Enter>");
            Console.WriteLine("To create a kafka message with a null key and UTF-8 encoded value:");
            Console.WriteLine("> value<enter>");
            Console.WriteLine("Ctrl-C to quit.\n");

            var cancelled = false;

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancelled = true;
            };

            while (!cancelled)
            {
                Console.Write("> ");

                var text = string.Empty;

                try
                {
                    text = Console.ReadLine();
                }
                catch (IOException)
                {
                    break;
                }

                if (string.IsNullOrWhiteSpace(text))
                {
                    break;
                }

                var key = string.Empty;
                var val = text;

                var index = text.IndexOf(" ");
                if (index != -1)
                {
                    key = text.Substring(0, index);
                    val = text.Substring(index + 1);
                }

                try
                {
                    var deliveryResult = await producer.ProduceAsync(topicName, new Message<string, string>
                    {
                        Key = key,
                        Value = val
                    });

                    Console.WriteLine($"delivered to: {deliveryResult.TopicPartitionOffset}");
                }
                catch (ProduceException<string, string> e)
                {
                    Console.WriteLine($"failed to deliver message: {e.Message} [{e.Error.Code}]");
                }
            }
        }
    }
}