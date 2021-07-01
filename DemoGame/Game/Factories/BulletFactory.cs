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
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;
        protected SpriteSheet SpriteSheet;

        #endregion

        #region Constructors

        public BulletFactory(SpriteSheet bulletSpriteSheet, IGraphicsService2D graphicsService2D)
        {
            SpriteSheet = bulletSpriteSheet;
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Public Methods

        public GameEntityBase CreateBullet(GameEntityBase parent)
        {
            var bullet = parent.AddChildEntity();
            bullet.AttachGameComponent(new BulletMovementComponent(GraphicsService2D));
            bullet.AttachGameComponent(new SpriteComponent(new Sprite(SpriteSheet, 0, 0, 32, 32, 32, 32), GraphicsService2D));
            bullet.Activate();

            return bullet;
        }

        #endregion
    }
}
