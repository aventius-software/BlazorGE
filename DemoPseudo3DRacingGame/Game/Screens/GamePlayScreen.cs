#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoPseudo3DRacingGame.Game.Systems.Debugging;
using DemoPseudo3DRacingGame.Game.Systems.Road;
using System.Threading.Tasks;

#endregion

namespace DemoPseudo3DRacingGame.Game.Screens
{
    public class GamePlayScreen : GameScreenBase
    {
        #region Protected Properties

        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService2D;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Constructors

        public GamePlayScreen(GameWorld gameWorld, IGraphicsService2D graphicsService2D, IGraphicAssetService graphicAssetService, IKeyboardService keyboardService) : base(gameWorld)
        {
            GraphicsService2D = graphicsService2D;
            GraphicAssetService = graphicAssetService;
            KeyboardService = keyboardService;
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
            GameWorld.AddGameSystem(new RoadDrawSystem(GraphicsService2D, KeyboardService));
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService2D));

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
