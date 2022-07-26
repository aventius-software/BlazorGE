#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Systems;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;
using BlazorGE.Input;

#endregion

namespace DemoGame.Game.Systems
{
    public class DebugSystem : IGameDrawSystem
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService;
        private readonly IMouseService _mouseService;

        #endregion

        #region Constructors

        public DebugSystem(IGraphicsService2D graphicsService, IMouseService mouseService)
        {
            GraphicsService = graphicsService;
            _mouseService = mouseService;
        }

        #endregion

        #region Override Methods

        public async ValueTask DrawAsync(GameTime gameTime)
        {
            var mouseState = _mouseService.GetState();
            // Just output a simple FPS text string (should do AVG fps really)
            await GraphicsService.DrawTextAsync($"FPS: {gameTime.FramesPerSecond}", 0, 30, "Arial", "red", 30, true);

            await GraphicsService.DrawTextAsync($"MouseCoords: X -> {mouseState.X} Y -> {mouseState.Y} LeftClick -> {(mouseState.KeyState == KeyState.Down ? "Pressed" : "Not pressed")}", 0, 60, "Arial", "red", 30, true);
        }

        #endregion
    }
}
