#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Input;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Components
{
    public class PlayerMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected KeyboardService KeyboardService;

        #endregion

        #region Public Properties

        public float Speed;

        #endregion

        #region Constructors

        public PlayerMovementComponent(KeyboardService keyboardService, float initialSpeed = 0.25f)
        {
            KeyboardService = keyboardService;
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
            // Get the current state of keyboard
            var kstate = KeyboardService.GetState();

            // Get the transform component
            var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();
            var direction = transformComponent.Direction;

            // Do we move left/right?
            if (kstate.IsKeyDown(Keys.LeftArrow)) direction.X = -1;
            else if (kstate.IsKeyDown(Keys.RightArrow)) direction.X = 1;

            // Do we move up/down?
            if (kstate.IsKeyDown(Keys.UpArrow)) direction.Y = -1;
            else if (kstate.IsKeyDown(Keys.DownArrow)) direction.Y = 1;

            // Move this entities position
            transformComponent.Translate(direction * Speed * gameTime.TimeSinceLastFrame);

            await Task.CompletedTask;
        }

        #endregion
    }
}
