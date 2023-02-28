#region Namespaces

using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;

#endregion

namespace BlazorGE.Game
{
    public class GameWorld
    {
        #region Public Properties

        public List<GameEntity> GameEntities { get; } = new();
        public List<IGameSystem> GameSystems { get; } = new();

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

                // Does this need initialising?
                if (gameSystem is IGameInitialisationSystem)
                {
                    Task.Run(async () => await ((IGameInitialisationSystem)gameSystem).InitialiseAsync());
                }
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
                    // Update all active entities
                    await geus.UpdateEntitiesAsync(gameTime, GameEntities.Where(geus.EntityPredicate()));

                    // Remove deactivated entities
                    GameEntities.RemoveAll(entity => !entity.IsActive);
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
