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
        protected int Score = 0;

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
            await GraphicsService.DrawTextAsync($"{Score}", (GraphicsService.CanvasWidth / 2) + 100, 150, "Arial", "white", 150, true);
        }

        /// <summary>
        /// We want the player, opposition and ball entities
        /// </summary>
        /// <returns></returns>
        public Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<ScoreComponent>() || entity.HasComponent<BallMovementComponent>();
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

            // Get the player
            var player = filteredGameEntities.Where(entity => entity.HasComponent<PlayerMovementComponent>()).Single();
            var playerScore = player.GetComponent<ScoreComponent>();

            // Is the ball in the oppositions goal?
            if (ballMovement.CurrentBallState == BallState.InOppositionsGoal)
            {
                // Yes, increment the players score
                playerScore.Score++;

                // Have we beaten the current high score?
                if (playerScore.Score > playerScore.HighScore) playerScore.HighScore = playerScore.Score;
            }

            // Save internally for drawing later
            Score = playerScore.Score;

            await Task.CompletedTask;
        }

        #endregion
    }
}
