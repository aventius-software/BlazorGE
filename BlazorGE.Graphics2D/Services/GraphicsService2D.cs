#region Namespaces

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics2D.Services
{
    public class GraphicsService2D : IGraphicsService2D
    {
        #region Private Constants

        protected const string ClearRect = "clearRect";
        protected const string DrawFilledPolygon = "drawFilledPolygon";
        protected const string DrawFilledRectangle = "drawFilledRectangle";
        protected const string DrawImage = "drawImage";
        protected const string DrawQuadrilateral = "drawQuadrilateral";
        protected const string DrawText = "drawText";
        protected const string DrawTrapezium = "drawTrapezium";

        #endregion

        #region Private Properties

        private readonly Lazy<Task<IJSObjectReference>> ModuleTask;

        #endregion

        #region Protected Properties

        protected List<BatchItem> BatchItems = new();
        protected bool UseBatching = false;

        #endregion

        #region Public Properties

        public int PlayFieldHeight { get; protected set; }
        public int PlayFieldWidth { get; protected set; }

        #endregion

        #region Constructors

        public GraphicsService2D(IJSRuntime jsRuntime)
        {
            // Need to look into improvements for interop and canvas, see this link
            // here https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#optimize-javascript-interop-speed
            ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorGE.Graphics2D/interop2d.js").AsTask());
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

        #region Batching

        /// <summary>
        /// Start batching graphics calls
        /// </summary>
        /// <returns></returns>
        public async ValueTask BeginBatchAsync()
        {
            UseBatching = true;
            BatchItems.Clear();

            await Task.CompletedTask;
        }

        /// <summary>
        /// End (or flush) all batched graphics calls
        /// </summary>
        /// <returns></returns>
        public async ValueTask EndBatchAsync()
        {
            UseBatching = false;
            var items = new object[BatchItems.Count];

            for (var i = 0; i < BatchItems.Count; i++)
            {
                items[i] = new object[] { BatchItems[i].FunctionName, BatchItems[i].Parameters };
            }

            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("drawBatch", new object[] { items });
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
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = ClearRect, Parameters = new object[] { x, y, width, height } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(ClearRect, x, y, width, height);
            }
        }

        /// <summary>
        /// Clear the screen
        /// </summary>
        /// <returns></returns>
        public async ValueTask ClearScreenAsync()
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = ClearRect, Parameters = new object[] { 0, 0, PlayFieldWidth, PlayFieldHeight } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(ClearRect, 0, 0, PlayFieldWidth, PlayFieldHeight);
            }
        }

        /// <summary>
        /// Draw a filled polygon
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public async ValueTask DrawFilledPolygonAsync(string colour, int[][] coordinates)
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawFilledPolygon, Parameters = new object[] { coordinates, colour } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawFilledPolygon, coordinates, colour);
            }
        }

        /// <summary>
        /// Draw a filled rectangle
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public async ValueTask DrawFilledRectangleAsync(string colour, int x, int y, int width, int height)
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawFilledRectangle, Parameters = new object[] { colour, x, y, width, height } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawFilledRectangle, colour, x, y, width, height);
            }
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
        public async ValueTask DrawImageAsync(ElementReference imageElementReference, int x, int y, int width, int height, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawImage, Parameters = new object[] { imageElementReference, sourceX, sourceY, sourceWidth, sourceHeight, x, y, width, height } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawImage, imageElementReference, sourceX, sourceY, sourceWidth, sourceHeight, x, y, width, height);
            }
        }

        /// <summary>
        /// Draw a quadrilateral
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns></returns>
        public async ValueTask DrawQuadrilateralAsync(string colour, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawQuadrilateral, Parameters = new object[] { colour, x1, y1, x2, y2, x3, y3, x4, y4 } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawQuadrilateral, colour, x1, y1, x2, y2, x3, y3, x4, y4);
            }
        }

        /// <summary>
        /// Draw the specified sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public async ValueTask DrawSpriteAsync(Sprite sprite)
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawImage, Parameters = new object[] { sprite.SpriteSheet.ImageElementReference, sprite.SourceX, sprite.SourceY, sprite.SourceWidth, sprite.SourceHeight, sprite.X, sprite.Y, sprite.Width, sprite.Height } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawImage, sprite.SpriteSheet.ImageElementReference, sprite.SourceX, sprite.SourceY, sprite.SourceWidth, sprite.SourceHeight, sprite.X, sprite.Y, sprite.Width, sprite.Height);
            }
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
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawText, Parameters = new object[] { text, x, y, $"{fontSize}px {fontFamily}", colour, isFilled } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawText, text, x, y, $"{fontSize}px {fontFamily}", colour, isFilled);
            }
        }

        /// <summary>
        /// Draws a trapezium (or trapezoid) which is a convex quadrilateral with a 
        /// pair of parallel sides (top and bottom)
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="x1">X coordinate of the centre of the bottom line of the trapezium</param>
        /// <param name="y1">Y coordinate of the centre of the bottom line of the trapezium</param>
        /// <param name="w1">Width of the top of the trapezium</param>
        /// <param name="x2">X coordinate of the centre of the top line of the trapezium</param>
        /// <param name="y2">Y coordinate of the centre of the top line of the trapezium</param>
        /// <param name="w2">Width of the bottom of the trapezium</param>
        /// <returns></returns>
        public async ValueTask DrawTrapeziumAsync(string colour, int x1, int y1, int w1, int x2, int y2, int w2)
        {
            if (UseBatching)
            {
                BatchItems.Add(new BatchItem { FunctionName = DrawTrapezium, Parameters = new object[] { colour, x1, y1, w1, x2, y2, w2 } });
            }
            else
            {
                var module = await ModuleTask.Value;
                await module.InvokeVoidAsync(DrawTrapezium, colour, x1, y1, w1, x2, y2, w2);
            }
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
