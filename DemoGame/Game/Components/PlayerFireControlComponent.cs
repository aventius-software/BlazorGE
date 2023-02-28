#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Input;
using DemoGame.Game.Factories;
using System.Numerics;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Components
{
    public class PlayerFireControlComponent : GameComponentBase, IUpdatableGameComponent
    {
        #region Protected Constants

        protected const int FiringInterval = 10;

        #endregion

        #region Protected Properties

        protected BulletFactory BulletFactory;
        protected IKeyboardService KeyboardService;
        protected int TimeSinceLastFiring = 0;

        #endregion

        #region Constructors

        public PlayerFireControlComponent(IKeyboardService keyboardService, BulletFactory bulletFactory)
        {
            KeyboardService = keyboardService;
            BulletFactory = bulletFactory;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Check for player firing bullet
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Increment the firing timer
            TimeSinceLastFiring++;

            // Have we reached or gone beyone the minimum interval between firing?
            if (TimeSinceLastFiring > FiringInterval)
            {
                // Yes, reset for next time
                TimeSinceLastFiring = 0;

                // Get the current state of keyboard
                var kstate = KeyboardService.GetState();

                // Has the player fired?
                if (kstate.IsKeyDown(Keys.Space))
                {
                    // Find the players transform component
                    var playerTransform = GameEntityOwner.GetComponent<Transform2DComponent>();

                    // Create a new bullet at the player current position
                    var bullet = BulletFactory.CreateBullet();                    
                    var bulletTransform = bullet.GetComponent<Transform2DComponent>();

                    // Calculate coordinates for where the bullet should appear above the player
                    var bulletX = playerTransform.Position.X + (playerTransform.Width / 2) - (bulletTransform.Width / 2);
                    var bulletY = playerTransform.Position.Y;

                    bulletTransform.Position = new Vector2(bulletX, bulletY);

                    // Finally, activate the bullet!                    
                    bullet.Activate();
                }
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
