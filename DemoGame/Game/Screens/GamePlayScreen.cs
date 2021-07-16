#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoGame.Game.Factories;
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
            // Create a factory to make bullets ;-)
            var bulletFactory = new BulletFactory(GameWorld, GraphicAssetService, GraphicsService2D);
            bulletFactory.LoadContent();

            // Create a factory to make the player(s)
            var playerFactory = new PlayerFactory(GameWorld, GraphicAssetService, GraphicsService2D, KeyboardService, bulletFactory);
            playerFactory.LoadContent();
            playerFactory.CreatePlayer(0, 0);

            // Create a factory to make enemies
            var enemyFactory = new EnemyFactory(GameWorld, GraphicAssetService, GraphicsService2D);
            enemyFactory.LoadContent();
            enemyFactory.CreateEnemy(50, 50);

            // Add all our systems to the world            
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService2D));
            GameWorld.AddGameSystem(new PlayerSystem());
            GameWorld.AddGameSystem(new EnemySystem());
            GameWorld.AddGameSystem(new BulletSystem(GameWorld));

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
