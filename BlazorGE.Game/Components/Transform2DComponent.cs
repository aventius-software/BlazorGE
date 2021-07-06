#region Namespaces

using System.Numerics;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public class Transform2DComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Public Properties

        public Vector2 Direction;
        public int Height;
        public Vector2 Position;
        public float Speed;
        public int Width;

        #endregion

        #region Constructors

        public Transform2DComponent()
        {
            Position = new Vector2(0, 0);
            Direction = new Vector2(0, 0);
            Speed = 0;
        }

        public Transform2DComponent(Vector2 position, Vector2 direction, float speed = 0, int width = 0, int height = 0)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            Width = width;
            Height = height;
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

        /// <summary>
        /// Returns true if the specified coordinates intersect with this transform
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IntersectsWith(int x, int y)
        {
            return x >= Position.X && x <= Position.X + Width && y >= Position.Y && y <= Position.Y + Height;
        }

        /// <summary>
        /// Returns true if this transforms bounding box intersects with the specified transforms bounding box
        /// </summary>
        /// <param name="transform2DComponent"></param>
        /// <returns></returns>
        public bool IntersectsWith(Transform2DComponent transform2DComponent)
        {
            return Position.X < transform2DComponent.Position.X + transform2DComponent.Width &&
                Position.X + Width > transform2DComponent.Position.X &&
                Position.Y < transform2DComponent.Position.Y + transform2DComponent.Height &&
                Position.Y + Height > transform2DComponent.Position.Y;
        }

        #endregion
    }
}
