using Grpc.Core;
using Grpc.Net.Client;
using Hello;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HttpTracker.gRPCClient.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new HelloService.HelloServiceClient(channel);

            await UnaryCallExample(client);

            await ServerStreamingCallExample(client);
        }

        private static async Task UnaryCallExample(HelloService.HelloServiceClient client)
        {
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "哈哈哈" });

            Console.WriteLine("Greeting: " + reply.Message);
        }

        private static async Task ServerStreamingCallExample(HelloService.HelloServiceClient client)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(3.5));

            using var call = client.SayHellos(new HelloRequest { Name = "啦啦啦" }, cancellationToken: cts.Token);

            try
            {
                await foreach (var message in call.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine("Greeting: " + message.Message);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }
        }
    }
}