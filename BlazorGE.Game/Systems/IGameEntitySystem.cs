#region Namespaces

using BlazorGE.Game.Entities;
using System;

#endregion

namespace BlazorGE.Game.Systems
{
    public interface IGameEntitySystem : IGameSystem
    {
        public Func<GameEntity, bool> EntityPredicate();
    }
}
