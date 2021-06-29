#region Namespaces

using BlazorGE.Core.Services;
using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace BlazorGE.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlazorgeServices<T>(this IServiceCollection services, bool useDefaultServices = true) where T : GameBase
        {
            // Register game instance
            services.AddSingleton<GameBase, T>();

            // See https://www.meziantou.net/optimizing-js-interop-in-a-blazor-webassembly-application.htm
            // Might be worth investigating for better JS and canvas performance?
            //services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            //services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());

            // Register other required services
            services.AddSingleton<GameService>();
            services.AddSingleton<GameWorld>();
            
            // For simplicity, we'll optionally add any services with default
            // implementations - unless the 'useDefaultServices' flag is set to false ;-)
            if (useDefaultServices)
            {
                services.AddSingleton<IGraphicAssetService, GraphicAssetService>();
                services.AddSingleton<IGameScreenService, GameScreenService>();
                services.AddSingleton<IGraphicsService2D, GraphicsService2D>();
                services.AddSingleton<IKeyboardService, KeyboardService>();
            }

            return services;
        }
    }
}
