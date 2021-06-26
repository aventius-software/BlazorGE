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

        protected GameScreenManager GameScreenManager;
        protected GameWorld GameWorld;
        protected GraphicsService GraphicsService;
        protected Keyboard Keyboard;

        #endregion

        #region Constructors

        public GameMain(GameScreenManager gameScreenManager, GameWorld gameWorld, GraphicsService graphicsService, Keyboard keyboard)
        {
            GameScreenManager = gameScreenManager;
            GameWorld = gameWorld;
            GraphicsService = graphicsService;
            Keyboard = keyboard;
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
            var screen = new GamePlayScreen(GameWorld, GraphicsService, Keyboard);
            await GameScreenManager.LoadScreenAsync(screen);

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
