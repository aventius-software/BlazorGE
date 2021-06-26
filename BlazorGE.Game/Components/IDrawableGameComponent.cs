#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public interface IDrawableGameComponent : IGameComponent
    {
        public ValueTask DrawAsync(GameTime gameTime);
    }
}
