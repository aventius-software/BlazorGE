﻿#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Screens
{
    public class GameScreenService : IGameScreenService
    {
        #region Protected Properties

        protected IGameScreen GameScreen = default!;

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the current game screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask DrawAsync(GameTime gameTime)
        {
            if (GameScreen is not null) await GameScreen.DrawAsync(gameTime);
        }

        /// <summary>
        /// Load a screen
        /// </summary>
        /// <param name="gameScreen"></param>
        /// <returns></returns>
        public async Task LoadScreenAsync(IGameScreen gameScreen)
        {
            // Unload if already a screen active
            await UnloadScreenAsync();

            // Save current game screen
            GameScreen = gameScreen;

            // Load any content            
            await GameScreen.LoadContentAsync();
        }

        /// <summary>
        /// Unload the current screen
        /// </summary>
        /// <returns></returns>
        public async Task UnloadScreenAsync()
        {
            if (GameScreen is not null)
            {                
                await GameScreen.UnloadContentAsync();
            }
        }

        /// <summary>
        /// Update the current screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            if (GameScreen is not null) await GameScreen.UpdateAsync(gameTime);
        }

        #endregion
    }
}
