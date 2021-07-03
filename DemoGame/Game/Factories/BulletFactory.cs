#region Namespaces

using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using DemoGame.Game.Components;

#endregion

namespace DemoGame.Game.Factories
{
    public class BulletFactory
    {
        #region Protected Constants

        protected const int Height = 32;
        protected const int Width = 32;

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

        public void Initialise()
        {
            SpriteSheet = GraphicAssetService.CreateSpriteSheet("images/bullet.png");
        }

        public GameEntityBase CreateBullet(GameEntityBase playerEntity)
        {
            var bullet = playerEntity.AddChildEntity();
            bullet.AttachGameComponent(new BulletMovementComponent(GraphicsService2D));
            bullet.AttachGameComponent(new SpriteComponent(new Sprite(SpriteSheet, 0, 0, Width, Height, Width, Height), GraphicsService2D));
            bullet.Activate();

            return bullet;
        }

        #endregion
    }
}
