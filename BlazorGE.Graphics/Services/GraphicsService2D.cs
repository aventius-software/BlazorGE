#region Namespaces

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Services
{
    public class GraphicsService2D : IAsyncDisposable, IGraphicsService2D
    {
        #region Private Properties

        private readonly Lazy<Task<IJSObjectReference>> ModuleTask;

        #endregion

        #region Public Properties

        public int PlayFieldHeight { get; set; }
        public int PlayFieldWidth { get; set; }

        #endregion

        #region Constructors

        public GraphicsService2D(IJSRuntime jsRuntime)
        {
            ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorGE.Graphics/interop.js").AsTask());
        }

        #endregion

        #region Public Methods

        public async ValueTask DisposeAsync()
        {
            if (ModuleTask.IsValueCreated)
            {
                var module = await ModuleTask.Value;
                await module.DisposeAsync();
            }
        }

        #endregion

        #region JSInvokable Methods

        /// <summary>
        /// Called by JS when the canvas resize event occurs
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [JSInvokable]
        public async ValueTask OnResizeCanvas(int width, int height)
        {
            PlayFieldWidth = width;
            PlayFieldHeight = height;

            await Task.CompletedTask;
        }

        #endregion

        #region Canvas Methods

        /// <summary>
        /// Clears a rectangle on the screen at specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public async ValueTask ClearRectangleAsync(int x, int y, int width, int height)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("clearRect", x, y, width, height);
        }

        /// <summary>
        /// Clear the screen
        /// </summary>
        /// <returns></returns>
        public async ValueTask ClearScreenAsync()
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("clearRect", 0, 0, PlayFieldWidth, PlayFieldHeight);
        }

        /// <summary>
        /// Draw image using the specified image element reference at the specified coordinates, with
        /// the specified dimensions
        /// </summary>
        /// <param name="imageElementReference"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="sourceX"></param>
        /// <param name="sourceY"></param>
        /// <param name="sourceWidth"></param>
        /// <param name="sourceHeight"></param>
        /// <returns></returns>
        public async ValueTask DrawImageAsync(
            ElementReference imageElementReference,
            int x,
            int y,
            int width,
            int height,
            int sourceX,
            int sourceY,
            int sourceWidth,
            int sourceHeight)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("drawImage", imageElementReference, sourceX, sourceY, sourceWidth, sourceHeight, x, y, width, height);
        }

        /// <summary>
        /// Draw the specified sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public async ValueTask DrawSpriteAsync(Sprite sprite)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync(
                "drawImage",
                sprite.SpriteSheet.ImageElementReference,
                sprite.SourceX,
                sprite.SourceY,
                sprite.SourceWidth,
                sprite.SourceHeight,
                sprite.X,
                sprite.Y,
                sprite.Width,
                sprite.Height);
        }

        /// <summary>
        /// Draw simple text using specified parameters
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="fontFamily"></param>
        /// <param name="colour"></param>
        /// <param name="fontSize"></param>
        /// <param name="isFilled"></param>
        /// <returns></returns>
        public async ValueTask DrawTextAsync(string text, int x, int y, string fontFamily, string colour, int fontSize, bool isFilled)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("drawText", text, x, y, $"{fontSize}px {fontFamily}", colour, isFilled);
        }

        /// <summary>
        /// Initialise the canvas for 2D operations
        /// </summary>
        /// <returns></returns>
        public async ValueTask InitialiseCanvas2D()
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("initialiseCanvas2D", DotNetObjectReference.Create(this));
        }

        #endregion
    }
}
