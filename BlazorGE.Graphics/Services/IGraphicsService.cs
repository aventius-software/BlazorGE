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
        #region Properties

        public int CanvasHeight { get; }
        public int CanvasWidth { get; }

        #endregion

        #region Batching

        public ValueTask BeginBatchAsync();
        public ValueTask EndBatchAsync();

        #endregion

        #region Event Handling

        public ValueTask OnResizeCanvas(int width, int height);
        

        #endregion
        
        #region Standard Methods

        public ValueTask ClearScreenAsync();        

        #endregion
    }
}
