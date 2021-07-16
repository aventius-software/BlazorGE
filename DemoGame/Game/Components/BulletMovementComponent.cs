#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Components
{
    public class BulletMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Constructors

        public BulletMovementComponent(IGraphicsService2D graphicsService2D)
        {
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Updates the bullet
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();

            // Deactivate if off screen
            if (transformComponent.Position.Y < 0) GameEntityOwner.Deactivate();

            await Task.CompletedTask;
        }

        #endregion
    }
}
