#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace BlazorGE.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlazorGEServices<TGame>(this IServiceCollection services, bool useDefaultServices = true) where TGame : GameBase
        {
            // Register game instance
            services.AddSingleton<GameBase, TGame>();

            // Register other required services            
            services.AddSingleton<GameWorld>();

            // For simplicity, we'll optionally add any services with default
            // implementations - unless the 'useDefaultServices' flag is set to false ;-)
            if (useDefaultServices)
            {
                services.AddSingleton<IGraphicAssetService, GraphicAssetService>();
                services.AddSingleton<IGameScreenService, GameScreenService>();
                services.AddSingleton<IGraphicsService2D, GraphicsService2D>();
                services.AddSingleton<IKeyboardService, KeyboardService>();
                services.AddSingleton<IMouseService, MouseService>();
            }

            return services;
        }
    }
}
