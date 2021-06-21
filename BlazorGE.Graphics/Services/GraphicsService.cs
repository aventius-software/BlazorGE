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

        public int Height { get; protected set; }
        public int Width { get; protected set; }

        #endregion

        #region Constructors

        public GraphicsService(IJSRuntime jsRuntime)
        {
            ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorGE.Graphics/interop.js").AsTask());
        }

        #endregion

        #region Public Methods

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

        public void RegisterOnCreateSpriteSheetHandler(Func<string, SpriteSheet> eventHandler)
        {
            if (OnCreateSpriteSheetHandlers != null) OnCreateSpriteSheetHandlers -= eventHandler;
            OnCreateSpriteSheetHandlers += eventHandler;
        }

        #endregion

        #region JSInvokable Methods

        [JSInvokable]
        public async ValueTask OnResizeCanvas(int width, int height)
        {
            Width = width;
            Height = height;

            await Task.CompletedTask;
        }

        #endregion

        #region Canvas Methods

        /// <summary>
        /// Clear a rectangle on the canvas at specified coordinates
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
