#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Systems
{
    public interface IGameUpdateSystem : IGameSystem
    {
        public ValueTask UpdateAsync(GameTime gameTime);
    }
}
