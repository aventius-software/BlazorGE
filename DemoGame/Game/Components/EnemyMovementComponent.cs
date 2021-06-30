#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Components
{
    public class EnemyMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService;

        #endregion

        #region Public Properties

        public float Speed;

        #endregion

        #region Constructors

        public EnemyMovementComponent(IGraphicsService2D keyboardService, float initialSpeed = 0.25f)
        {
            GraphicsService = keyboardService;
            Speed = initialSpeed;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {            
            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();
            var direction = transformComponent.Direction;

            // Change direction if hit the side of the screen
            if (transformComponent.Position.X > GraphicsService.PlayFieldWidth || transformComponent.Position.X < 0) direction.X *= -1;
            if (transformComponent.Position.Y > GraphicsService.PlayFieldHeight || transformComponent.Position.Y < 0) direction.Y *= -1;

            await Task.CompletedTask;
        }

        #endregion
    }
}
