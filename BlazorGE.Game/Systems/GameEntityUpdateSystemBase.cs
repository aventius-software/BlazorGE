#region Namespace

using BlazorGE.Game.Entities;

#endregion

namespace BlazorGE.Game.Systems
{
    public abstract class GameEntityUpdateSystemBase : IGameEntityUpdateSystem
    {
        /// <summary>
        /// Update all active entities
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public virtual async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            for (var i = 0; i < filteredGameEntities.Count(); i++)
            {
                await filteredGameEntities.ElementAt(i).UpdateAsync(gameTime);
            }
        }

        /// <summary>
        /// Implement this to return your desired entites to work with
        /// </summary>
        /// <returns></returns>
        public abstract Func<GameEntity, bool> EntityPredicate();
    }
}
