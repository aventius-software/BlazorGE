#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Systems
{
    public interface IGameDrawSystem : IGameSystem
    {
        public ValueTask DrawAsync(GameTime gameTime);
    }
}
