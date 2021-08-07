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

        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            await GraphicsService.DrawTextAsync("Welcome to BlazorGE! Press space to start the game!", 50, 50, "Arial", "red", 30, true);

            await base.DrawAsync(gameTime);
        }

        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            var kstate = KeyboardService.GetState();

            if (kstate.IsKeyDown(Keys.Space))
            {
                await GameScreenManager.LoadScreenAsync(new GamePlayScreen(GameWorld, GraphicsService, GraphicAssetService, KeyboardService));
            }

            await base.UpdateAsync(gameTime);
        }

        #endregion
    }
}
