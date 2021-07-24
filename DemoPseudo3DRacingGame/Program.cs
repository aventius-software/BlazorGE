#region Namespaces

using BlazorGE.Core.Extensions;
using DemoPseudo3DRacingGame.Game;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DemoPseudo3DRacingGame
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Add BlazorGE services and register our game
            builder.Services.AddBlazorGEServices<GameMain>();

            await builder.Build().RunAsync();
        }
    }
}
