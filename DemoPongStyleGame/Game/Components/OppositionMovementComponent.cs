#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Components
{
    public class OppositionMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

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
            var direction = transformComponent.Direction;
            
            // Move this entity         
            //transformComponent.Translate(direction * Speed * gameTime.TimeSinceLastFrame);

            // Check we've not gone out of bounds            
            if (transformComponent.Position.Y < 0) transformComponent.Position.Y = 0;
            else if (transformComponent.Position.Y > GraphicsService2D.CanvasHeight - transformComponent.Height)
                transformComponent.Position.Y = GraphicsService2D.CanvasHeight - transformComponent.Height;

            await Task.CompletedTask;
        }

        #endregion
    }
}
