#region Namespaces

using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using DemoPongStyleGame.Game.Components;
using System;

#endregion

namespace DemoPongStyleGame.Game.Systems
{
    public class PlayerSystem : GameEntityDrawAndUpdateSystemBase
    {
        #region Override Methods
        
        /// <summary>
        /// This system should only act upon the player entities
        /// </summary>
        /// <returns></returns>
        public override Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<PlayerMovementComponent>();
        }

        #endregion
    }
}
