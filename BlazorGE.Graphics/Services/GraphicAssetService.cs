#region Namespaces

using System;

#endregion

namespace BlazorGE.Graphics.Services
{
    public class GraphicAssetService
    {
        #region Private Properties

        private event Func<string, SpriteSheet> OnCreateSpriteSheetHandlers;

        #endregion        

        #region Public Methods

        /// <summary>
        /// Create/load a new sprite from the specified url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public SpriteSheet CreateSpriteSheet(string url)
        {
            return OnCreateSpriteSheetHandlers.Invoke(url);
        }

        /// <summary>
        /// Register event handler for sprite sheet create event
        /// </summary>
        /// <param name="eventHandler"></param>
        public void RegisterOnCreateSpriteSheetHandler(Func<string, SpriteSheet> eventHandler)
        {
            if (OnCreateSpriteSheetHandlers != null) OnCreateSpriteSheetHandlers -= eventHandler;
            OnCreateSpriteSheetHandlers += eventHandler;
        }

        #endregion
    }
}
