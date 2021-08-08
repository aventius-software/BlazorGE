#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Screens
{
    public class WelcomeScreen : GameScreenBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Constructors

        public WelcomeScreen(
            GameWorld gameWorld,
            IGraphicsService2D graphicsService,
            IKeyboardService keyboardService,
            IGameScreenService gameScreenManager,
            IGraphicAssetService graphicAssetService) : base(gameWorld)
        {
            GraphicsService = graphicsService;
            KeyboardService = keyboardService;
            GameScreenManager = gameScreenManager;
            GraphicAssetService = graphicAssetService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draw the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            // Just a simple welcome message
            await GraphicsService.DrawTextAsync("Welcome to BlazorGE! Press space to start the game!", 50, 50, "Arial", "red", 30, true);

            await base.DrawAsync(gameTime);
        }

        /// <summary>
        /// Update the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Get the current state of the keyboard
            var kstate = KeyboardService.GetState();

            // Has the user pressed space bar?
            if (kstate.IsKeyDown(Keys.Space))
            {
                // Yes, load the game play screen!
                await GameScreenManager.LoadScreenAsync(new GamePlayScreen(GameWorld, GraphicsService, KeyboardService));
            }

            await base.UpdateAsync(gameTime);
        }

        #endregion
    }
}
