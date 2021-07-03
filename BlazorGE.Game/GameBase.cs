#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game
{
    public class GameBase
    {
        #region Public Methods

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async ValueTask DrawAsync(GameTime gameTime) => await Task.CompletedTask;

        /// <summary>
        /// Load any content for the game
        /// </summary>
        /// <returns></returns>
        public virtual async Task LoadContentAsync() => await Task.CompletedTask;

        /// <summary>
        /// Unload any content for the game
        /// </summary>
        /// <returns></returns>
        public virtual async Task UnloadContentAsync() => await Task.CompletedTask;

        /// <summary>
        /// Update the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async ValueTask UpdateAsync(GameTime gameTime) => await Task.CompletedTask;

        #endregion
    }
}
