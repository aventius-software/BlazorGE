#region Namespaces

using BlazorGE.Graphics.Assets;
using System;

#endregion

namespace BlazorGE.Graphics.Services
{
    public class GraphicAssetService : IGraphicAssetService
    {
        #region Private Properties

        private event Func<string, GraphicAsset> OnCreateGraphicAssetHandlers;

        #endregion        

        #region Public Methods

        /// <summary>
        /// Create/load a new graphic asset from the specified url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GraphicAsset CreateGraphicAsset(string url)
        {
            return OnCreateGraphicAssetHandlers.Invoke(url);
        }

        /// <summary>
        /// Register event handler for asset create event
        /// </summary>
        /// <param name="eventHandler"></param>
        public void RegisterOnCreateGraphicAssetHandler(Func<string, GraphicAsset> eventHandler)
        {
            if (OnCreateGraphicAssetHandlers != null) OnCreateGraphicAssetHandlers -= eventHandler;
            OnCreateGraphicAssetHandlers += eventHandler;
        }

        #endregion
    }
}
