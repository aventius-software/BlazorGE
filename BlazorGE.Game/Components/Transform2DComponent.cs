#region Namespaces

using System.Numerics;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public class Transform2DComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Public Properties

        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;

        #endregion

        #region Constructors

        public Transform2DComponent()
        {
            Position = new Vector2(0, 0);
            Direction = new Vector2(0, 0);
            Speed = 0;
        }

        public Transform2DComponent(Vector2 position, Vector2 direction, float rate = 0)
        {
            Position = position;
            Direction = direction;
            Speed = rate;
        }

        #endregion

        #region Implementations

        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Update the position
            Position += Direction * Speed * gameTime.TimeSinceLastFrame;

            // TODO: 'Scale'?
            await Task.CompletedTask;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Translate the transform using specified parameter
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector2 translation)
        {
            Position += translation;
        }

        #endregion
    }
}
