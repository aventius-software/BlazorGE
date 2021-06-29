#region Namespaces

using System;

#endregion

namespace BlazorGE.Graphics.Services
{
    public interface IGraphicAssetService
    {
        public SpriteSheet CreateSpriteSheet(string url);
        public void RegisterOnCreateSpriteSheetHandler(Func<string, SpriteSheet> eventHandler);
    }
}
