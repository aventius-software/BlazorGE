#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
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
        protected SpriteSheet SpriteSheet;

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
            SpriteSheet = GraphicAssetService.CreateSpriteSheet("images/ballBlue.png");
        }

        public GameEntity CreateEnemy(int startX, int startY)
        {
            var enemy = GameWorld.CreateGameEntity();
            enemy.AttachGameComponent(new EnemyMovementComponent(GraphicsService2D));
            enemy.AttachGameComponent(new Transform2DComponent(new Vector2(startX, startY), new Vector2(1, 1), 0.25f, Width, Height));
            enemy.AttachGameComponent(new SpriteComponent(new Sprite(SpriteSheet, 0, 0, Width, Height, Width, Height), GraphicsService2D));
            enemy.Activate();

            return enemy;
        }

        #endregion
    }
}
