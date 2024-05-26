#region Namespaces

using BlazorGE.Graphics2D.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using static System.Net.Mime.MediaTypeNames;

#endregion

namespace BlazorGE.Graphics2D.Components
{    
    /// <summary>
    /// Canvas 2D interop functions, for more details about JS interop, see the links below
    /// https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/import-export-interop?view=aspnetcore-8.0
    /// https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/location-of-javascript?view=aspnetcore-8.0
    /// </summary>
    [SupportedOSPlatform("browser")]
    public partial class Canvas2D
    {
        #region Injected Services
        
        [Inject]
        private IGraphicsService2D GraphicsService { get; set; }

        #endregion

        #region Private Fields

        private ElementReference CanvasReference;
        private static Canvas2D Self;

        #endregion

        private const string CanvasID = "blazorge-canvas";

        #region Override Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            if (!firstRender) return;
                        
            await CanvasReference.FocusAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            // Load the canvas 2D module
            await JSHost.ImportAsync("canvas2D", $"/_content/BlazorGE.Graphics2D/canvas2D.js");
            
            // Initialise some stuff on the JS side
            InitialiseModule("BlazorGE.Graphics2D", nameof(Canvas2D), CanvasID);

            // Save a static reference to this object
            Self = this;
        }

        #endregion

        #region JS Import Interop Methods

        [JSImport("clearRect", "canvas2D")]
        internal static partial void ClearRect(
            [JSMarshalAs<JSType.Number>] int x,
            [JSMarshalAs<JSType.Number>] int y,
            [JSMarshalAs<JSType.Number>] int width,
            [JSMarshalAs<JSType.Number>] int height);

        [JSImport("drawFilledRectangle", "canvas2D")]
        internal static partial void DrawFilledRectangle(
            [JSMarshalAs<JSType.String>] string colour,
            [JSMarshalAs<JSType.Number>] int x,
            [JSMarshalAs<JSType.Number>] int y,
            [JSMarshalAs<JSType.Number>] int width,
            [JSMarshalAs<JSType.Number>] int height);

        [JSImport("drawImage", "canvas2D")]
        internal static partial void DrawImage(
            [JSMarshalAs<JSType.String>] string imageElementReference, // Not sure
            [JSMarshalAs<JSType.Number>] int sourceX,
            [JSMarshalAs<JSType.Number>] int sourceY,
            [JSMarshalAs<JSType.Number>] int sourceWidth,
            [JSMarshalAs<JSType.Number>] int sourceHeight,
            [JSMarshalAs<JSType.Number>] int x,
            [JSMarshalAs<JSType.Number>] int y,
            [JSMarshalAs<JSType.Number>] int width,
            [JSMarshalAs<JSType.Number>] int height);

        [JSImport("drawQuadrilateral", "canvas2D")]
        internal static partial void DrawQuadrilateral(
            [JSMarshalAs<JSType.String>] string colour,
            [JSMarshalAs<JSType.Number>] int x1,
            [JSMarshalAs<JSType.Number>] int y1,
            [JSMarshalAs<JSType.Number>] int x2,
            [JSMarshalAs<JSType.Number>] int y2,
            [JSMarshalAs<JSType.Number>] int x3,
            [JSMarshalAs<JSType.Number>] int y3,
            [JSMarshalAs<JSType.Number>] int x4,
            [JSMarshalAs<JSType.Number>] int y4);              

        [JSImport("drawRect", "canvas2D")]
        internal static partial void DrawRectangle(
            [JSMarshalAs<JSType.String>] string colour,
            [JSMarshalAs<JSType.Number>] int x,
            [JSMarshalAs<JSType.Number>] int y,
            [JSMarshalAs<JSType.Number>] int width,
            [JSMarshalAs<JSType.Number>] int height);

        [JSImport("drawTrapezium", "canvas2D")]
        internal static partial void DrawTrapezium(
            [JSMarshalAs<JSType.String>] string colour,
            [JSMarshalAs<JSType.Number>] int x1,
            [JSMarshalAs<JSType.Number>] int y1,
            [JSMarshalAs<JSType.Number>] int w1,
            [JSMarshalAs<JSType.Number>] int x2,
            [JSMarshalAs<JSType.Number>] int y2,
            [JSMarshalAs<JSType.Number>] int w2);

        [JSImport("drawText", "canvas2D")]
        internal static partial void DrawText(
            [JSMarshalAs<JSType.String>] string text,
            [JSMarshalAs<JSType.Number>] int x,
            [JSMarshalAs<JSType.Number>] int y,
            [JSMarshalAs<JSType.String>] string font,
            [JSMarshalAs<JSType.String>] string colour,
            [JSMarshalAs<JSType.Boolean>] bool isFilled);

        [JSImport("initialiseModule", "canvas2D")]        
        internal static partial void InitialiseModule(
            [JSMarshalAs<JSType.String>] string assemblyName,
            [JSMarshalAs<JSType.String>] string componentName,
            [JSMarshalAs<JSType.String>] string canvasID);

        #endregion

        #region JS Export Interop Methods

        [JSExport]
        internal static void OnResizeCanvas(int width, int height)
        {
            Task.Run(async () => await Self.GraphicsService.OnResizeCanvas(width, height));
        }
        
        #endregion
    }
}
