#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics2D.Services;
using System.Numerics;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Components
{
    public class OppositionMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected Vector2 BallPosition;
        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Public Properties

        public float Speed;

        #endregion

        #region Constructors

        public OppositionMovementComponent(IGraphicsService2D graphicsService2D, float initialSpeed = 0.25f)
        {
            GraphicsService2D = graphicsService2D;
            Speed = initialSpeed;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Updates the opposition bat
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();

            // Move the bat according to the ball position... need to add some 'dumbness' to this
            // so that it makes some mistakes occasionally
            if (BallPosition.Y < transformComponent.Position.Y) transformComponent.Direction.Y = -1;
            else if (BallPosition.Y > transformComponent.Position.Y) transformComponent.Direction.Y = 1;

            // Move this entity         
            transformComponent.Translate(transformComponent.Direction * Speed * gameTime.TimeSinceLastFrame);

            await Task.CompletedTask;
        }

        #endregion

        /// <summary>
        /// Tell this component where the ball is ;-)
        /// </summary>
        /// <param name="ballPosition"></param>
        public void SpecifyBallPosition(Vector2 ballPosition)
        {
            BallPosition = ballPosition;
        }
    }
}
