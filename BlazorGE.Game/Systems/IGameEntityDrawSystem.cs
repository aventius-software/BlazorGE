#region Namespaces

using BlazorGE.Game.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Systems
{
    public interface IGameEntityDrawSystem : IGameEntitySystem
    {
        public ValueTask DrawEntitiesAsync(GameTime gameTime, IEnumerable<GameEntityBase> filteredGameEntities);
    }
}
