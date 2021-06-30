#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Components
{
    public class PlayerMovementComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService2D;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Public Properties

        public float Speed;

        #endregion

        #region Constructors

        public PlayerMovementComponent(IKeyboardService keyboardService, IGraphicsService2D graphicsService2D, float initialSpeed = 0.25f)
        {
            KeyboardService = keyboardService;
            GraphicsService2D = graphicsService2D;
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

            // Move this entity         
            transformComponent.Translate(direction * Speed * gameTime.TimeSinceLastFrame);

            // Check we've not gone out of bounds
            var spriteComponent = GameEntityOwner.GetComponent<SpriteComponent>();

            if (transformComponent.Position.X < 0) transformComponent.Position.X = 0;
            else if (transformComponent.Position.X > GraphicsService2D.PlayFieldWidth - spriteComponent.Sprite.Width) 
                transformComponent.Position.X = GraphicsService2D.PlayFieldWidth - spriteComponent.Sprite.Width;

            if (transformComponent.Position.Y < 0) transformComponent.Position.Y = 0;
            else if (transformComponent.Position.Y > GraphicsService2D.PlayFieldHeight - spriteComponent.Sprite.Height) 
                transformComponent.Position.Y = GraphicsService2D.PlayFieldHeight - spriteComponent.Sprite.Height;

            await Task.CompletedTask;
        }

        #endregion
    }
}
