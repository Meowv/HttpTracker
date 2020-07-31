using Confluent.Kafka;
using System;
using System.Linq;
using System.Threading;

namespace HttpTracker.KafkaConsumer.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: .. brokerList topicName");
                return;
            }

            var brokerList = args.First();
            var topicName = args.Last();

            Console.WriteLine($"Started consumer, Ctrl-C to stop consuming");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var config = new ConsumerConfig
            {
                BootstrapServers = brokerList,
                GroupId = "consumer",
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

            const int commitPeriod = 5;

            using var consumer = new ConsumerBuilder<Ignore, string>(config)
                                 .SetErrorHandler((_, e) =>
                                 {
                                     Console.WriteLine($"Error: {e.Reason}");
                                 })
                                 .SetStatisticsHandler((_, json) =>
                                 {
                                     Console.WriteLine($" - {DateTime.Now:yyyy-MM-dd HH:mm:ss} > monitoring..");
                                     //Console.WriteLine($"Statistics: {json}");
                                 })
                                 .SetPartitionsAssignedHandler((c, partitions) =>
                                 {
                                     Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                                 })
                                 .SetPartitionsRevokedHandler((c, partitions) =>
                                 {
                                     Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                                 })
                                 .Build();
            consumer.Subscribe(topicName);

            try
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cts.Token);

                        if (consumeResult.IsPartitionEOF)
                        {
                            Console.WriteLine($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                            continue;
                        }

                        Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                        if (consumeResult.Offset % commitPeriod == 0)
                        {
                            try
                            {
                                consumer.Commit(consumeResult);
                            }
                            catch (KafkaException e)
                            {
                                Console.WriteLine($"Commit error: {e.Error.Reason}");
                            }
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Consume error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Closing consumer.");
                consumer.Close();
            }
        }
    }
}