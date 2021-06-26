#region Namespaces

using BlazorGE.Game.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Systems
{
    public interface IGameEntityUpdateSystem : IGameEntitySystem
    {
        public ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntityBase> filteredGameEntities);
    }
}
