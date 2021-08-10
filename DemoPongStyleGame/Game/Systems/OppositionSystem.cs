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
    public class OppositionSystem : GameEntityDrawAndUpdateSystemBase
    {
        #region Override Methods

        /// <summary>
        /// This system should only act upon the opposition entities, but we want to reference 
        /// the ball entity
        /// </summary>
        /// <returns></returns>
        public override Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<OppositionMovementComponent>()
                || entity.HasComponent<BallMovementComponent>();
        }

        /// <summary>
        /// Update the opposition entity
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public override async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            // We want to reference the ball position
            var ball = filteredGameEntities.Where(entity => entity.HasComponent<BallMovementComponent>()).Single();
            var ballPosition = ball.GetComponent<Transform2DComponent>().Position;

            // Give the ball position to the opposition movement component so it can decide where to move
            var opposition = filteredGameEntities.Where(entity => entity.HasComponent<OppositionMovementComponent>()).Single();
            opposition.GetComponent<OppositionMovementComponent>().SpecifyBallPosition(ballPosition);

            // Make sure we only actually update the opposition entity (not the ball!)
            await base.UpdateEntitiesAsync(gameTime, filteredGameEntities.Where(entity => entity.HasComponent<OppositionMovementComponent>()));
        }

        #endregion
    }
}
