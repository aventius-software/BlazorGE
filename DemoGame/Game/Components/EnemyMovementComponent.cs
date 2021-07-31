#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Components
{
    public class EnemyMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Constructors

        public EnemyMovementComponent(IGraphicsService2D graphicsService2D)
        {
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Updates the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();

            // Change direction if hit the side of the screen
            if (transformComponent.Position.X > GraphicsService2D.CanvasWidth || transformComponent.Position.X < 0) transformComponent.Direction.X *= -1;
            if (transformComponent.Position.Y > GraphicsService2D.CanvasHeight || transformComponent.Position.Y < 0) transformComponent.Direction.Y *= -1;

            await Task.CompletedTask;
        }

        #endregion
    }
}
