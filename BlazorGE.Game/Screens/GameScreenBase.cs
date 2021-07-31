#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Screens
{
    public class GameScreenBase : IGameScreen
    {
        #region Protected Properties

        protected GameWorld GameWorld;

        #endregion

        #region Constructors

        public GameScreenBase(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }

        #endregion

        #region Implementations

        /// <summary>
        /// Draw the current screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async ValueTask DrawAsync(GameTime gameTime) => await GameWorld.DrawAsync(gameTime);
        
        /// <summary>
        /// Load content for the current screen
        /// </summary>
        /// <returns></returns>
        public virtual async Task LoadContentAsync() => await Task.CompletedTask;

        /// <summary>
        /// Unload content for the current screen
        /// </summary>
        /// <returns></returns>
        public virtual async Task UnloadContentAsync() => await Task.CompletedTask;

        /// <summary>
        /// Update the current screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async ValueTask UpdateAsync(GameTime gameTime) => await GameWorld.UpdateAsync(gameTime);

        #endregion
    }
}
