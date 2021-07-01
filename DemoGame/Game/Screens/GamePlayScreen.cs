#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using DemoGame.Game.Components;
using DemoGame.Game.Factories;
using DemoGame.Game.Systems;
using System.Numerics;
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
            // Load sprite sheet for the enemy
            var bulletSpriteSheet = GraphicAssetService.CreateSpriteSheet("images/bullet.png");

            // Create a factory to make bullets ;-)
            var bulletFactory = new BulletFactory(bulletSpriteSheet, GraphicsService2D);

            // Load sprite sheet for the player
            var playerSpriteSheet = GraphicAssetService.CreateSpriteSheet("images/player.png");

            // Create the player entity, attach components and activate ;-)
            var player = GameWorld.CreateGameEntity();
            player.AttachGameComponent(new PlayerMovementComponent(KeyboardService, GraphicsService2D));
            player.AttachGameComponent(new Transform2DComponent());
            player.AttachGameComponent(new SpriteComponent(new Sprite(playerSpriteSheet, 0, 0, 59, 59, 59, 59, 50, 50), GraphicsService2D));
            player.AttachGameComponent(new PlayerFireControlComponent(KeyboardService, bulletFactory));
            player.Activate();

            // Load sprite sheet for the enemy
            var enemySpriteSheet = GraphicAssetService.CreateSpriteSheet("images/enemy.png");

            // Create enemy
            var enemy = GameWorld.CreateGameEntity();
            enemy.AttachGameComponent(new EnemyMovementComponent(GraphicsService2D));
            enemy.AttachGameComponent(new Transform2DComponent(new Vector2(50, 50), new Vector2(1, 1), 0.25f));
            enemy.AttachGameComponent(new SpriteComponent(new Sprite(enemySpriteSheet, 0, 0, 59, 59, 59, 59), GraphicsService2D));
            enemy.Activate();

            // Add all our systems to the world            
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService2D));
            GameWorld.AddGameSystem(new PlayerSystem());
            GameWorld.AddGameSystem(new EnemySystem());
            GameWorld.AddGameSystem(new BulletSystem());

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
