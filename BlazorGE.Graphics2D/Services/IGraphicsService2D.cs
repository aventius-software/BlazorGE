#region Namespaces

using BlazorGE.Graphics.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics2D.Services
{
    public interface IGraphicsService2D : IAsyncDisposable, IGraphicsService
    {
        #region JSInvokable Methods

        [JSInvokable]
        public ValueTask OnResizeCanvas(int width, int height);

        #endregion

        #region Properties

        public int PlayFieldHeight { get; }
        public int PlayFieldWidth { get; }

        #endregion

        #region Canvas Methods

        public ValueTask ClearRectangleAsync(int x, int y, int width, int height);
        public ValueTask ClearScreenAsync();
        public ValueTask DrawFilledPolygonAsync(string colour, int[][] coordinates);
        public ValueTask DrawFilledRectangleAsync(string colour, int x, int y, int width, int height);
        public ValueTask DrawImageAsync(ElementReference imageElementReference, int x, int y, int width, int height, int sourceX, int sourceY, int sourceWidth, int sourceHeight);
        public ValueTask DrawQuadrilateralAsync(string colour, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4);
        public ValueTask DrawSpriteAsync(Sprite sprite);
        public ValueTask DrawTextAsync(string text, int x, int y, string fontFamily, string colour, int fontSize, bool isFilled);
        public ValueTask DrawTrapeziumAsync(string colour, int x1, int y1, int w1, int x2, int y2, int w2);
        public ValueTask InitialiseCanvas2D(ElementReference canvasReference);

        #endregion
    }
}
