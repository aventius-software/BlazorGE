#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Components
{
    public enum BallState
    {
        InPlay,
        InPlayersGoal,
        InOppositionsGoal
    }

    public class BallMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Public Properties

        public BallState CurrentBallState { get; protected set; } = BallState.InPlay;

        #endregion

        #region Constructors

        public BallMovementComponent(IGraphicsService2D graphicsService2D)
        {
            GraphicsService2D = graphicsService2D;            
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Updates the ball
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();

            // Is the ball in anyones goal?
            if (transformComponent.Position.X < 0 - transformComponent.Width)
            {
                CurrentBallState = BallState.InOppositionsGoal;
            }
            else if (transformComponent.Position.X > GraphicsService2D.CanvasWidth + transformComponent.Width)
            {
                CurrentBallState = BallState.InPlayersGoal;
            }

            // Change direction if hit the top/bottom of the screen
            if (transformComponent.Position.Y > (GraphicsService2D.CanvasHeight - transformComponent.Height)
                || transformComponent.Position.Y < 0) transformComponent.Direction.Y *= -1;

            await Task.CompletedTask;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This just resets the ball ready for the next game
        /// </summary>
        public void ResetBall()
        {
            // Reset the state of the ball
            CurrentBallState = BallState.InPlay;

            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();
            transformComponent.Position.X = GraphicsService2D.CanvasWidth / 2;
            transformComponent.Position.Y = GraphicsService2D.CanvasHeight / 2;
        }

        #endregion
    }
}
