#region Namespace

using BlazorGE.Game.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Systems
{
    public abstract class GameEntityDrawSystemBase : IGameEntityDrawSystem
    {
        /// <summary>
        /// Draw all active entities
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public virtual async ValueTask DrawEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            foreach (var entity in filteredGameEntities)
            {
                await entity.DrawAsync(gameTime);
            }
        }

        /// <summary>
        /// Implement this to return your desired entites to work with
        /// </summary>
        /// <returns></returns>
        public abstract Func<GameEntity, bool> EntityPredicate();
    }
}
