#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Components
{
    public class PlayerDrawComponent : GameComponentBase, IDrawableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Constructors

        public PlayerDrawComponent(IGraphicsService2D graphicsService2D)
        {
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask DrawAsync(GameTime gameTime)
        {
            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();            

            // Draw rectangle for the player ;-)
            await GraphicsService2D.DrawFilledRectangleAsync(
                "white", 
                (int)transformComponent.Position.X, 
                (int)transformComponent.Position.Y,
                transformComponent.Width,
                transformComponent.Height);
        }

        #endregion
    }
}
