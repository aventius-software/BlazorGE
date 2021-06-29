#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using DemoGame.Game.Components;
using DemoGame.Game.Systems;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Screens
{
    public class GamePlayScreen : GameScreenBase
    {
        #region Protected Properties

        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;
        protected SpriteSheet PlayerSpriteSheet;

        #endregion

        #region Constructors

        public GamePlayScreen(GameWorld gameWorld, IGraphicsService2D graphicsService, IGraphicAssetService graphicAssetService, IKeyboardService keyboardService) : base(gameWorld)
        {
            GraphicsService = graphicsService;
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
            // Load sprite sheet for the player
            PlayerSpriteSheet = GraphicAssetService.CreateSpriteSheet("images/player.png");

            // Create the player entity, attach components and activate ;-)
            var player = GameWorld.CreateGameEntity();
            player.AttachGameComponent(new PlayerMovementComponent(KeyboardService));
            player.AttachGameComponent(new Transform2DComponent());
            player.AttachGameComponent(new SpriteComponent(new Sprite(PlayerSpriteSheet, 0, 0, 59, 59, 59, 59, 50, 50), GraphicsService));
            player.Activate();

            // Add the debugging and player game systems to the world            
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService));
            GameWorld.AddGameSystem(new PlayerSystem());

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
