using Grpc.Core;
using Hello;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Server.Services
{
    public class GreeterService : HelloService.HelloServiceBase
    {
        private readonly ILogger _logger;

        public GreeterService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GreeterService>();
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Sending hello to {request.Name}");

            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }

        public override async Task SayHellos(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var message = $"How are you {request.Name}? {++i}";

                _logger.LogInformation($"Sending greeting {message}.");

                await responseStream.WriteAsync(new HelloReply { Message = message });

                await Task.Delay(1000);
            }
        }
    }
}