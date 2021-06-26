#region Namespaces

using System.Numerics;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public class Transform2DComponent : IUpdatableGameComponent
    {
        #region Public Properties

        public Vector2 Position;
        public Vector2 Speed;

        #endregion

        #region Constructors

        public Transform2DComponent()
        {
            Position = new Vector2(0, 0);
            Speed = new Vector2(0, 0);
        }

        public Transform2DComponent(Vector2 position, Vector2 speed)
        {
            Position = position;
            Speed = speed;
        }

        #endregion

        #region Interface Implementations

        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Update the position
            Position = Vector2.Add(Position, Speed);

            // TODO: 'Scale'?
            await Task.CompletedTask;
        }

        #endregion
    }
}
