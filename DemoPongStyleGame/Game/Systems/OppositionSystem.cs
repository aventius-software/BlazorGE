#region Namespaces

using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using DemoPongStyleGame.Game.Components;
using System;

#endregion

namespace DemoPongStyleGame.Game.Systems
{
    public class OppositionSystem : GameEntityDrawAndUpdateSystemBase
    {
        #region Override Methods

        /// <summary>
        /// This system should only act upon the opposition entities
        /// </summary>
        /// <returns></returns>
        public override Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<OppositionMovementComponent>();
        }

        #endregion
    }
}
