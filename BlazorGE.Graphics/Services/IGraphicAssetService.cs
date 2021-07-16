#region Namespaces

using BlazorGE.Graphics.Assets;
using System;

#endregion

namespace BlazorGE.Graphics.Services
{
    public interface IGraphicAssetService
    {
        public GraphicAsset CreateGraphicAsset(string url);
        public void RegisterOnCreateGraphicAssetHandler(Func<string, GraphicAsset> eventHandler);
    }
}
