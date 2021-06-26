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

        protected GraphicsService GraphicsService;
        protected Keyboard Keyboard;
        protected SpriteSheet PlayerSpriteSheet;

        #endregion

        #region Constructors

        public GamePlayScreen(GameWorld gameWorld, GraphicsService graphicsService, Keyboard keyboard) : base(gameWorld)
        {
            GraphicsService = graphicsService;
            Keyboard = keyboard;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Load content for this game screen
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            // Load sprite sheet for player
            PlayerSpriteSheet = GraphicsService.CreateSpriteSheet("images/player.png");

            // Create the player entity and attach components
            var player = GameWorld.CreateGameEntity();
            player.AttachGameComponent(new PlayerComponent());
            player.AttachGameComponent(new Transform2DComponent());
            player.AttachGameComponent(new SpriteComponent(new Sprite(PlayerSpriteSheet, 0, 0, 59, 59, 59, 59, 50, 50), GraphicsService));
            player.Activate();

            // Add the debugging and player game systems to the world            
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService));
            GameWorld.AddGameSystem(new PlayerSystem(Keyboard));

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
