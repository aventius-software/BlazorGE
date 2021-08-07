#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoPongStyleGame.Game.Factories;
using DemoPongStyleGame.Game.Systems;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Screens
{
    public class GamePlayScreen : GameScreenBase
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Constructors

        public GamePlayScreen(
            GameWorld gameWorld,
            IGraphicsService2D graphicsService2D,
            IKeyboardService keyboardService) : base(gameWorld)
        {
            GraphicsService2D = graphicsService2D;
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
            // Create a factory to make the player(s)
            var playerFactory = new PlayerFactory(GameWorld, GraphicsService2D, KeyboardService);
            playerFactory.CreatePlayer();

            // Create a factory to make the ball(s)
            var ballFactory = new BallFactory(GameWorld, GraphicsService2D);
            ballFactory.CreateBall();

            // Add all our systems to the world
            GameWorld.AddGameSystem(new ArenaSystem(GraphicsService2D));
            GameWorld.AddGameSystem(new PlayerSystem());
            GameWorld.AddGameSystem(new BallSystem());
            GameWorld.AddGameSystem(new CollisionSystem());
            GameWorld.AddGameSystem(new ScoringSystem(GraphicsService2D));

            // Lastly add debugging to show FPS on top of everything
            GameWorld.AddGameSystem(new DebugSystem(GraphicsService2D));

            // And...
            await base.LoadContentAsync();
        }

        #endregion
    }
}
