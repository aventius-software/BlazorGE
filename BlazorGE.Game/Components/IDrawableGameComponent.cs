#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public interface IDrawableGameComponent : IGameComponent
    {
        /// <summary>
        /// Implement this to add any drawing functionality for your component
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public ValueTask DrawAsync(GameTime gameTime);
    }
}
