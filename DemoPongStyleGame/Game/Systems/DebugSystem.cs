#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Systems;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Systems
{
    public class DebugSystem : IGameDrawSystem
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService;

        #endregion

        #region Constructors

        public DebugSystem(IGraphicsService2D graphicsService)
        {
            GraphicsService = graphicsService;
        }

        #endregion

        #region Override Methods

        public async ValueTask DrawAsync(GameTime gameTime)
        {
            // Just output a simple FPS text string (should do AVG fps really)
            await GraphicsService.DrawTextAsync($"FPS: {gameTime.FramesPerSecond}", 0, 30, "Arial", "red", 30, true);
        }

        #endregion
    }
}
