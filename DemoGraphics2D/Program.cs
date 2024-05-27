using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DemoGraphics2D
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Add graphics service, also requires mouse service to handle canvas mouse events
            builder.Services.AddSingleton<IMouseService, MouseService>();
            builder.Services.AddSingleton<IGraphicsService2D, GraphicsService2D>();

            await builder.Build().RunAsync();
        }
    }
}
