#region Namespaces

using BlazorGE.Core.Extensions;
using DemoOutrunStyleGame.Game;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace DemoOutrunStyleGame
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Standard start...
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Register HttpClient
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Add BlazorGE services and register our game
            builder.Services.AddBlazorgeServices<GameMain>();

            // And go...
            await builder.Build().RunAsync();
        }
    }
}
