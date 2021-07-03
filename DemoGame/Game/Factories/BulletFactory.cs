#region Namespaces

using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using DemoGame.Game.Components;
using System.Numerics;

#endregion

namespace DemoGame.Game.Factories
{
    public class BulletFactory
    {
        #region Protected Constants

        protected const int Height = 20;
        protected const int Width = 21;

        #endregion

        #region Protected Properties

        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService2D;
        protected SpriteSheet SpriteSheet;

        #endregion

        #region Constructors

        public BulletFactory(IGraphicAssetService graphicAssetService, IGraphicsService2D graphicsService2D)
        {
            GraphicAssetService = graphicAssetService;
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Public Methods

        public void LoadContent()
        {
            // Sprite downloaded from https://opengameart.org/content/puzzle-game-art credit to 'Kenney.nl'
            SpriteSheet = GraphicAssetService.CreateSpriteSheet("images/particleStar.png");
        }

        public GameEntityBase CreateBullet(GameEntityBase playerEntity)
        {
            var bullet = playerEntity.AddChildEntity();
            bullet.AttachGameComponent(new BulletMovementComponent(GraphicsService2D));            
            bullet.AttachGameComponent(new Transform2DComponent(new Vector2(0, 0), new Vector2(0, -1), 0.25f, Width, Height));
            bullet.AttachGameComponent(new SpriteComponent(new Sprite(SpriteSheet, 0, 0, Width, Height, Width, Height), GraphicsService2D));

            return bullet;
        }

        #endregion
    }
}
