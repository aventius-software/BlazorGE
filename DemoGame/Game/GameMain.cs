#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using DemoGame.Game.Screens;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game
{
    public class GameMain : GameBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Constructors

        public GameMain(GameWorld gameWorld, IGameScreenService gameScreenManager, IGraphicsService2D graphicsService, IGraphicAssetService graphicAssetService, IKeyboardService keyboardService)
        {
            GameWorld = gameWorld;
            GameScreenManager = gameScreenManager;            
            GraphicsService = graphicsService;
            GraphicAssetService = graphicAssetService;
            KeyboardService = keyboardService;
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
            // First clear the screen, then draw the current game screen
            await GraphicsService.ClearScreenAsync();
            await GameScreenManager.DrawAsync(gameTime);

            await base.DrawAsync(gameTime);
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            // Create our screen and load it
            //var screen = new GamePlayScreen(GameWorld, GraphicsService, GraphicAssetService, KeyboardService);            
            await GameScreenManager.LoadScreenAsync(new WelcomeScreen(GameWorld, GraphicsService, KeyboardService, GameScreenManager, GraphicAssetService));

            await base.LoadContentAsync();
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
