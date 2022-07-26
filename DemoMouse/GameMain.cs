#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoMouse.Screens;

#endregion

namespace DemoMouse
{
    public class GameMain : GameBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IMouseService MouseService;

        #endregion

        #region Constructors

        public GameMain(GameWorld gameWorld, IGameScreenService gameScreenManager, IGraphicsService2D graphicsService, IGraphicAssetService graphicAssetService, IMouseService mouseService)
        {
            GameWorld = gameWorld;
            GameScreenManager = gameScreenManager;
            GraphicsService = graphicsService;
            GraphicAssetService = graphicAssetService;
            MouseService = mouseService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            // Start batching graphics calls
            await GraphicsService.BeginBatchAsync();

            // Clear screen and draw the game...
            await GraphicsService.ClearScreenAsync();
            await GameScreenManager.DrawAsync(gameTime);

            await base.DrawAsync(gameTime);

            // End/flush batched graphics calls
            await GraphicsService.EndBatchAsync();
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            // Create our screen and load it                      
            await GameScreenManager.LoadScreenAsync(new WelcomeScreen(GameWorld, GraphicsService, MouseService, GameScreenManager, GraphicAssetService));

            await base.LoadContentAsync();
        }

        /// <summary>
        /// Unload/dispose of any resources
        /// </summary>
        /// <returns></returns>
        public override async Task UnloadContentAsync()
        {
            // Unload/dispose of any resources
            await GameScreenManager.UnloadScreenAsync();

            await base.UnloadContentAsync();
        }

        /// <summary>
        /// Update the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Update the current game screen
            await GameScreenManager.UpdateAsync(gameTime);

            await base.UpdateAsync(gameTime);
        }

        #endregion
    }
}
