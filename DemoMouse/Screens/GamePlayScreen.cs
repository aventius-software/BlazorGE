#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoGame.Game.Systems;

#endregion

namespace DemoMouse.Screens
{
    public class GamePlayScreen : GameScreenBase
    {
        #region Protected Properties

        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService2D;
        protected IMouseService MouseService;

        #endregion

        #region Constructors

        public GamePlayScreen(GameWorld gameWorld, IGraphicsService2D graphicsService2D, IGraphicAssetService graphicAssetService, IMouseService mouseService) : base(gameWorld)
        {
            GraphicsService2D = graphicsService2D;
            GraphicAssetService = graphicAssetService;
            MouseService = mouseService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Load content for this game screen
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            // Add all our systems to the world            
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService2D, MouseService));

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
