#region Namespace

using BlazorGE.Game.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            foreach (var entity in filteredGameEntities)
            {
                await entity.UpdateAsync(gameTime);
            }
        }

        /// <summary>
        /// Implement this to return your desired entites to work with
        /// </summary>
        /// <returns></returns>
        public abstract Func<GameEntity, bool> EntityPredicate();
    }
}
