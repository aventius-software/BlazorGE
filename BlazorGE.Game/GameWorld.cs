#region Namespaces

using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game
{


    public class GameWorld
    {
        #region Public Properties

        public List<GameEntity> GameEntities { get; } = new List<GameEntity>();
        public List<IGameSystem> GameSystems { get; } = new List<IGameSystem>();

        #endregion        

        #region Public Methods

        /// <summary>
        /// Add or register a game system
        /// </summary>
        /// <typeparam name="TGameSystem"></typeparam>
        /// <param name="gameSystem"></param>
        public void AddGameSystem<TGameSystem>(TGameSystem gameSystem) where TGameSystem : IGameSystem
        {
            // Add system, but only if its not already added ;-)
            if (!GameSystems.Any(gameSystem => gameSystem is TGameSystem))
            {
                GameSystems.Add(gameSystem);
            }
        }

        /// <summary>
        /// Create a game entity
        /// </summary>
        /// <returns></returns>
        public GameEntity CreateGameEntity()
        {
            var gameEntity = new GameEntity();
            GameEntities.Add(gameEntity);

            return gameEntity;
        }

        /// <summary>
        /// Destroy the specified game entity
        /// </summary>
        /// <param name="gameEntity"></param>
        public void DestroyGameEntity(GameEntity gameEntity)
        {
            gameEntity.Deactivate(true);
            GameEntities.RemoveAll(entity => entity.Id == gameEntity.Id);
        }

        /// <summary>
        /// Draw all active game systems
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async ValueTask DrawAsync(GameTime gameTime)
        {
            foreach (var gameSystem in GameSystems)
            {
                if (gameSystem is IGameEntityDrawSystem geds)
                {
                    await geds.DrawEntitiesAsync(gameTime, GameEntities.Where(geds.EntityPredicate()));
                }
                else if (gameSystem is IGameDrawSystem gds)
                {
                    await gds.DrawAsync(gameTime);
                }
            }
        }

        /// <summary>
        /// Update all active game systems
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async ValueTask UpdateAsync(GameTime gameTime)
        {
            foreach (var gameSystem in GameSystems)
            {
                if (gameSystem is IGameEntityUpdateSystem geus)
                {
                    await geus.UpdateEntitiesAsync(gameTime, GameEntities.Where(geus.EntityPredicate()));
                }
                else if (gameSystem is IGameUpdateSystem gus)
                {
                    await gus.UpdateAsync(gameTime);
                }
            }
        }

        #endregion
    }
}
