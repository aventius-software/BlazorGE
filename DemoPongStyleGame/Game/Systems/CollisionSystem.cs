#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using DemoPongStyleGame.Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Systems
{
    public class CollisionSystem : IGameEntityUpdateSystem
    {
        #region Implementations

        /// <summary>
        /// We want the ball, player and opposition entities to check for collisions between them
        /// </summary>
        /// <returns></returns>
        public Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<BallMovementComponent>()
                || entity.HasComponent<PlayerMovementComponent>()
                || entity.HasComponent<OppositionMovementComponent>();
        }

        /// <summary>
        /// Check for collisions between the ball and player/opposition bats
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            // Get the ball
            var ball = filteredGameEntities.Where(entity => entity.HasComponent<BallMovementComponent>()).Single();
            var ballTransform = ball.GetComponent<Transform2DComponent>();

            // Get the player
            var player = filteredGameEntities.Where(entity => entity.HasComponent<PlayerMovementComponent>()).Single();
            var playerTransform = player.GetComponent<Transform2DComponent>();

            // Get the opposition
            var opposition = filteredGameEntities.Where(entity => entity.HasComponent<OppositionMovementComponent>()).Single();
            var oppositionTransform = opposition.GetComponent<Transform2DComponent>();

            // Has the ball hit the players bat?
            if (ballTransform.IntersectsWith(playerTransform) || ballTransform.IntersectsWith(oppositionTransform))
            {
                // Yes, invert its X direction
                ballTransform.Direction.X *= -1;
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
