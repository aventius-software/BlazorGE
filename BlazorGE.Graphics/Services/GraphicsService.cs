#region Namespaces

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Services
{
    public class GraphicsService : IAsyncDisposable
    {
        #region Private Properties

        private readonly Lazy<Task<IJSObjectReference>> ModuleTask;
        private event Func<string, SpriteSheet> OnCreateSpriteSheetHandlers;

        #endregion

        #region Public Properties

        public int PlayFieldHeight { get; protected set; }
        public int PlayFieldWidth { get; protected set; }

        #endregion

        #region Constructors

        public GraphicsService(IJSRuntime jsRuntime)
        {
            ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorGE.Graphics/interop.js").AsTask());
        }

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

        public async ValueTask DisposeAsync()
        {
            if (ModuleTask.IsValueCreated)
            {
                var module = await ModuleTask.Value;
                await module.DisposeAsync();
            }
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
        /// Clears a rectangle on the canvas at specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public async ValueTask ClearRectAsync(int x, int y, int width, int height)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("clearRect", x, y, width, height);
        }

        /// <summary>
        /// Draw image using the specified img reference at coordinates
        /// </summary>
        /// <param name="reference"></param>
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
            ElementReference reference,
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
            await module.InvokeVoidAsync("drawImage", reference, sourceX, sourceY, sourceWidth, sourceHeight, x, y, width, height);
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
            await module.InvokeVoidAsync("drawText", text, x, y, colour, $"{fontSize}px {fontFamily.ToLower()}", isFilled);            
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
