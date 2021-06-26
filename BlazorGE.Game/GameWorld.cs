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

        public List<GameEntityBase> GameEntities { get; } = new List<GameEntityBase>();        
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
            // TODO: check if system is already added!
            GameSystems.Add(gameSystem);
        }

        /// <summary>
        /// Create a game entity
        /// </summary>
        /// <returns></returns>
        public GameEntityBase CreateGameEntity()
        {
            var gameEntity = new GameEntityBase();
            GameEntities.Add(gameEntity);

            return gameEntity;
        }

        /// <summary>
        /// Destroy game entity
        /// </summary>
        /// <param name="gameEntity"></param>
        public void DestroyGameEntity(GameEntityBase gameEntity) => GameEntities.Remove(gameEntity);

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
