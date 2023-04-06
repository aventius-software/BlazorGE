#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public interface IUpdatableGameComponent : IGameComponent
    {
        /// <summary>
        /// Implement this to add any update logic for your game component
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public ValueTask UpdateAsync(GameTime gameTime);
    }
}
