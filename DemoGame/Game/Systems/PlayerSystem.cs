#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using BlazorGE.Input;
using DemoGame.Game.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game.Systems
{
    public class PlayerSystem : GameEntityDrawSystemBase, IGameEntityUpdateSystem
    {
        #region Protected Properties

        protected Keyboard Keyboard;

        #endregion

        #region Constructors

        public PlayerSystem(Keyboard keyboard)
        {
            Keyboard = keyboard;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// This system should only act upon the player entities
        /// </summary>
        /// <returns></returns>
        public override Func<GameEntityBase, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<PlayerComponent>();
        }

        /// <summary>
        /// Update the player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntityBase> filteredGameEntities)
        {
            // Get the current keyboard state
            var kstate = Keyboard.GetState();

            // Update each entity
            foreach (var entity in filteredGameEntities)
            {
                // Update transform position
                var transformComponent = entity.GetComponent<Transform2DComponent>();

                // Move left/right
                if (kstate.IsKeyDown(Keys.LeftArrow)) transformComponent.Position.X--;
                else if (kstate.IsKeyDown(Keys.RightArrow)) transformComponent.Position.X++;

                // Move up/down
                if (kstate.IsKeyDown(Keys.UpArrow)) transformComponent.Position.Y--;
                else if (kstate.IsKeyDown(Keys.DownArrow)) transformComponent.Position.Y++;

                // Update sprite position
                var spriteComponent = entity.GetComponent<SpriteComponent>();
                spriteComponent.Sprite.X = (int)transformComponent.Position.X;
                spriteComponent.Sprite.Y = (int)transformComponent.Position.Y;

                // Update this entity
                await entity.UpdateAsync(gameTime);
            }
        }

        #endregion
    }
}
