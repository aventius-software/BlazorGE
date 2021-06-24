#region Namespaces

using BlazorGE.Core.Services;
using BlazorGE.Game;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace BlazorGE.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlazorgeServices<T>(this IServiceCollection services) where T : GameBase
        {
            // Register game instance
            services.AddSingleton<GameBase, T>();

            // See https://www.meziantou.net/optimizing-js-interop-in-a-blazor-webassembly-application.htm
            // Might be worth investigating for better JS and canvas performance?
            //services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            //services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());

            // Register managers
            services.AddSingleton<CoreInteropService>();
            services.AddSingleton<GraphicsService>();
            services.AddSingleton<Keyboard>();

            return services;
        }
    }
}
