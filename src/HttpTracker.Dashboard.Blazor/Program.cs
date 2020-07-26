using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpTracker.Dashboard.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var baseAddress = "https://localhost:44390"; //builder.HostEnvironment.BaseAddress

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

            builder.Services.AddAntDesign();

            await builder.Build().RunAsync();
        }
    }
}