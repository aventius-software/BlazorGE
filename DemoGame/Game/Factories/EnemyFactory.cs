#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics.Assets;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D;
using BlazorGE.Graphics2D.Services;
using DemoGame.Game.Components;
using System.Numerics;

#endregion

namespace DemoGame.Game.Factories
{
    public class EnemyFactory
    {
        #region Protected Constants

        protected const int Height = 22;
        protected const int Width = 22;

        #endregion

        #region Protected Properties

        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService2D;
        protected GraphicAsset GraphicAsset;

        #endregion

        #region Constructors

        public EnemyFactory(GameWorld gameWorld, IGraphicAssetService graphicAssetService, IGraphicsService2D graphicsService2D)
        {
            GameWorld = gameWorld;
            GraphicAssetService = graphicAssetService;
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Public Methods

        public void LoadContent()
        {
            // Sprite downloaded from https://opengameart.org/content/puzzle-game-art credit to 'Kenney.nl'
            GraphicAsset = GraphicAssetService.CreateGraphicAsset("images/ballBlue.png");
        }

        public GameEntity CreateEnemy(int startX, int startY)
        {
            var enemy = GameWorld.CreateGameEntity();
            enemy.AttachGameComponent(new EnemyMovementComponent(GraphicsService2D));
            enemy.AttachGameComponent(new Transform2DComponent(new Vector2(startX, startY), new Vector2(1, 1), 0.25f, Width, Height));
            enemy.AttachGameComponent(new SpriteComponent(new Sprite(GraphicAsset, 0, 0, Width, Height, Width, Height), GraphicsService2D));
            enemy.Activate();

            return enemy;
        }

        #endregion
    }
}
