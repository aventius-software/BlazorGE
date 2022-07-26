#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;

#endregion

namespace DemoMouse.Screens
{
    public class WelcomeScreen : GameScreenBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IMouseService MouseService;
        protected MouseState LastMouseState;

        #endregion

        #region Constructors

        public WelcomeScreen(GameWorld gameWorld, IGraphicsService2D graphicsService, IMouseService mouseService, IGameScreenService gameScreenManager, IGraphicAssetService graphicAssetService) : base(gameWorld)
        {
            GraphicsService = graphicsService;
            MouseService = mouseService;
            GameScreenManager = gameScreenManager;
            GraphicAssetService = graphicAssetService;
            LastMouseState = new MouseState(0, 0, KeyState.Up);
        }

        #endregion

        #region Override Methods

        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            await GraphicsService.DrawTextAsync("Welcome to BlazorGE! Press mouse button to start the game!", 50, 50, "Arial", "red", 30, true);

            await base.DrawAsync(gameTime);
        }

        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            var mouseState = MouseService.GetState();

            if (LastMouseState.KeyState == KeyState.Down && mouseState.KeyState == KeyState.Up)
            {
                await GameScreenManager.LoadScreenAsync(new GamePlayScreen(GameWorld, GraphicsService, GraphicAssetService, MouseService));
            }

            LastMouseState = mouseState;

            await base.UpdateAsync(gameTime);
        }

        #endregion
    }
}
