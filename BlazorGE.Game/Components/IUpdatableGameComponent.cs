#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public interface IUpdatableGameComponent : IGameComponent
    {
        public ValueTask UpdateAsync(GameTime gameTime);
    }
}
