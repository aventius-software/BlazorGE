using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGE.ECS.Components
{
    public interface IUpdatableGameComponent : IGameComponent
    {
        public Task UpdateAsync(GameTime gameTime, GameContext gameContext);
    }
}
