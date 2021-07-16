#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics.Assets;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoGame.Game.Components;

#endregion

namespace DemoGame.Game.Factories
{
    public class PlayerFactory
    {
        #region Protected Constants

        protected const int Height = 48;
        protected const int Width = 46;

        #endregion

        #region Protected Properties

        protected BulletFactory BulletFactory;
        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService2D;
        protected IKeyboardService KeyboardService;
        protected GraphicAsset GraphicAsset;

        #endregion

        #region Constructors

        public PlayerFactory(GameWorld gameWorld, IGraphicAssetService graphicAssetService, IGraphicsService2D graphicsService2D, IKeyboardService keyboardService, BulletFactory bulletFactory)
        {
            GameWorld = gameWorld;
            GraphicAssetService = graphicAssetService;
            GraphicsService2D = graphicsService2D;
            KeyboardService = keyboardService;
            BulletFactory = bulletFactory;
        }

        #endregion

        #region Public Methods

        public void LoadContent()
        {
            // Sprite downloaded from https://opengameart.org/content/puzzle-game-art credit to 'Kenney.nl'
            GraphicAsset = GraphicAssetService.CreateGraphicAsset("images/element_grey_polygon.png");
        }

        public GameEntity CreatePlayer(int startX, int startY)
        {
            var player = GameWorld.CreateGameEntity();
            player.AttachGameComponent(new PlayerMovementComponent(KeyboardService, GraphicsService2D));
            player.AttachGameComponent(new Transform2DComponent { Width = Width, Height = Height });
            player.AttachGameComponent(new SpriteComponent(new Sprite(GraphicAsset, 0, 0, Width, Height, Width, Height, startX, startY), GraphicsService2D));
            player.AttachGameComponent(new PlayerFireControlComponent(KeyboardService, BulletFactory));
            player.AttachGameComponent(new PlayerScoreComponent());
            player.Activate();

            return player;
        }

        #endregion
    }
}
