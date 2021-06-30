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
            // Load sprite sheet for the player
            var playerSpriteSheet = GraphicAssetService.CreateSpriteSheet("images/player.png");

            // Create the player entity, attach components and activate ;-)
            var player = GameWorld.CreateGameEntity();
            player.AttachGameComponent(new PlayerMovementComponent(KeyboardService, GraphicsService2D));
            player.AttachGameComponent(new Transform2DComponent());
            player.AttachGameComponent(new SpriteComponent(new Sprite(playerSpriteSheet, 0, 0, 59, 59, 59, 59, 50, 50), GraphicsService2D));
            player.Activate();

            // Load sprite sheet for the enemy
            //var enemySpriteSheet = GraphicAssetService.CreateSpriteSheet("images/enemy.png");

            //// Create enemy
            //var enemy = GameWorld.CreateGameEntity();
            //enemy.AttachGameComponent(new EnemyMovementComponent(GraphicsService));
            //enemy.AttachGameComponent(new Transform2DComponent());
            //enemy.AttachGameComponent(new SpriteComponent(new Sprite(enemySpriteSheet, 0, 0, 59, 59, 59, 59, 50, 50), GraphicsService));

            // Add the debugging and player game systems to the world            
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService2D));
            GameWorld.AddGameSystem(new PlayerSystem());
            //GameWorld.AddGameSystem(new EnemySystem());

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
