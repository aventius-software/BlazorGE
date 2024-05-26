#region Namespaces

using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Runtime.Versioning;

#endregion

namespace BlazorGE.Graphics2D.Services
{
    public class GraphicsService2D : IGraphicsService2D
    {        
        #region Public Properties

        public int CanvasHeight { get; protected set; }
        public int CanvasWidth { get; protected set; }        

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
        [SupportedOSPlatform("browser")]
        public async ValueTask ClearRectangleAsync(int x, int y, int width, int height)
        {
            Canvas2D.ClearRect(x, y, width, height);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Clear the screen
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform("browser")]
        public async ValueTask ClearScreenAsync()
        {
            Canvas2D.ClearRect(0, 0, CanvasWidth, CanvasHeight);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Draw a filled polygon
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawFilledPolygonAsync(string colour, int[][] coordinates)
        {
            //Canvas2D.DrawFilledPolygon(coordinates, colour);
            await Task.CompletedTask;
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
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawFilledRectangleAsync(string colour, int x, int y, int width, int height)
        {
            Canvas2D.DrawFilledRectangle(colour, x, y, width, height);
            await Task.CompletedTask;
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
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawImageAsync(ElementReference imageElementReference, int x, int y, int width, int height, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            //Canvas2D.DrawImage(imageElementReference, sourceX, sourceY, sourceWidth, sourceHeight, x, y, width, height);
            await Task.CompletedTask;
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
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawQuadrilateralAsync(string colour, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            Canvas2D.DrawQuadrilateral(colour, x1, y1, x2, y2, x3, y3, x4, y4);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Draw the specified sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawSpriteAsync(Sprite sprite)
        {
            //Canvas2D.DrawImage(sprite.SpriteSheet.ImageElementReference, sprite.SourceX, sprite.SourceY, sprite.SourceWidth, sprite.SourceHeight, sprite.X, sprite.Y, sprite.Width, sprite.Height);
            await Task.CompletedTask;
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
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawTextAsync(string text, int x, int y, string fontFamily, string colour, int fontSize, bool isFilled)
        {
            Canvas2D.DrawText(text, x, y, $"{fontSize}px {fontFamily}", colour, isFilled);
            await Task.CompletedTask;
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
        [SupportedOSPlatform("browser")]
        public async ValueTask DrawTrapeziumAsync(string colour, int x1, int y1, int w1, int x2, int y2, int w2)
        {
            Canvas2D.DrawTrapezium(colour, x1, y1, w1, x2, y2, w2);
            await Task.CompletedTask;
        }

        #endregion

        #region Implementations
              
        public async ValueTask OnResizeCanvas(int width, int height)
        {
            CanvasWidth = width;
            CanvasHeight = height;

            await Task.CompletedTask;
        }
        
        public async ValueTask BeginBatchAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask EndBatchAsync()
        {
            await Task.CompletedTask;
        }
        
        #endregion
    }
}
