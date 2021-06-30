#region Namespaces

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Services
{
    public interface IGraphicsService2D
    {
        #region JSInvokable Methods

        [JSInvokable]
        public ValueTask OnResizeCanvas(int width, int height);

        #endregion

        public int PlayFieldHeight { get; set; }
        public int PlayFieldWidth { get; set; }

        #region Canvas Methods

        public ValueTask ClearRectangleAsync(int x, int y, int width, int height);
        public ValueTask ClearScreenAsync();
        public ValueTask DrawImageAsync(ElementReference imageElementReference, int x, int y, int width, int height, int sourceX, int sourceY, int sourceWidth, int sourceHeight);
        public ValueTask DrawSpriteAsync(Sprite sprite);
        public ValueTask DrawTextAsync(string text, int x, int y, string fontFamily, string colour, int fontSize, bool isFilled);
        public ValueTask InitialiseCanvas2D();

        #endregion
    }
}
