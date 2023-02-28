#region Namespaces

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Services
{
    public interface IGraphicsService
    {
        #region JSInvokable Methods

        [JSInvokable]
        public ValueTask OnResizeCanvas(int width, int height);

        #endregion

        #region Properties

        public int CanvasHeight { get; }
        public int CanvasWidth { get; }

        public event EventHandler<ElementReference> OnInitialised;

        #endregion

        #region Batching

        public ValueTask BeginBatchAsync();
        public ValueTask EndBatchAsync();

        #endregion

        #region Standard Methods

        public ValueTask ClearScreenAsync();
        public ValueTask InitialiseCanvasAsync(ElementReference canvasReference);

        #endregion
    }
}
