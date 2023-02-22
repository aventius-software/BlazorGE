#region Namespaces

using BlazorGE.Core.Extensions;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoGame.Game;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace DemoGame
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

            // Add BlazorGE services and register our game, specify 'false' for 'useCustomServices'...
            builder.Services.AddBlazorGEServices<GameMain>(false);

            // ...then supply your own implementations here. If you omit the above parameter
            // then BlazorGE will use its own default implementations and you don't need to 
            // specify them here. This is just here as an example...
            builder.Services.AddSingleton<IGraphicAssetService, GraphicAssetService>();
            builder.Services.AddSingleton<IGameScreenService, GameScreenService>();
            builder.Services.AddSingleton<IGraphicsService2D, GraphicsService2D>();
            builder.Services.AddSingleton<IKeyboardService, KeyboardService>();
            builder.Services.AddSingleton<IMouseService, MouseService>();

            // And go...
            await builder.Build().RunAsync();
        }
    }
}
