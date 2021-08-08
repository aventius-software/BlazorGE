#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using BlazorGE.Graphics2D.Services;
using DemoPongStyleGame.Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Systems
{
    public class ScoringSystem : IGameDrawSystem, IGameEntityUpdateSystem
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService;
        protected int OppositionScore = 0;
        protected int PlayerScore = 0;

        #endregion

        #region Constructors

        public ScoringSystem(IGraphicsService2D graphicsService)
        {
            GraphicsService = graphicsService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draw the score on the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask DrawAsync(GameTime gameTime)
        {
            // Draw the player score
            await GraphicsService.DrawTextAsync($"{PlayerScore}", (GraphicsService.CanvasWidth / 2) + 100, 150, "Arial", "white", 150, true);

            // Draw the opposition score
            await GraphicsService.DrawTextAsync($"{OppositionScore}", (GraphicsService.CanvasWidth / 2) - 200, 150, "Arial", "white", 150, true);
        }

        /// <summary>
        /// We want the ball entity
        /// </summary>
        /// <returns></returns>
        public Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<BallMovementComponent>();
        }

        /// <summary>
        /// Update the scores if someone scores ;-)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            // Get the ball
            var ball = filteredGameEntities.Where(entity => entity.HasComponent<BallMovementComponent>()).Single();
            var ballMovement = ball.GetComponent<BallMovementComponent>();

            // Is the ball in the oppositions goal?
            if (ballMovement.CurrentBallState == BallState.InOppositionsGoal)
            {
                // Yes, increment the players score
                PlayerScore++;
            }
            else if (ballMovement.CurrentBallState == BallState.InPlayersGoal)
            {
                // Ball is in the players goal, so we increment the opposition score
                OppositionScore++;
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
