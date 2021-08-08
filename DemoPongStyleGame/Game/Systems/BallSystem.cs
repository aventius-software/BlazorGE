#region Namespaces

using BlazorGE.Game;
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
    public class BallSystem : GameEntityDrawAndUpdateSystemBase
    {
        #region Override Methods

        /// <summary>
        /// This system should only act upon the ball
        /// </summary>
        /// <returns></returns>
        public override Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<BallMovementComponent>();
        }

        /// <summary>
        /// Updates the ball if it needs to be reset after a goal
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public override async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            // Get the ball
            var ball = filteredGameEntities.Single();
            var ballMovement = ball.GetComponent<BallMovementComponent>();

            // If its gone in someones goal, then reset
            if (ballMovement.CurrentBallState is not BallState.InPlay)
            {
                ballMovement.ResetBall();
            }

            await base.UpdateEntitiesAsync(gameTime, filteredGameEntities);
        }

        #endregion               
    }
}
