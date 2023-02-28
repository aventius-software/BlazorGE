#region Namespaces

using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

#endregion

namespace BlazorGE.Core.Services
{
    internal class InternalGameInteropService : IAsyncDisposable
    {
        #region Private Properties

        private readonly Lazy<Task<IJSObjectReference>> ModuleTask;

        #endregion

        #region Constructors

        public InternalGameInteropService(IJSRuntime jsRuntime)
        {
            ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorGE.Core/interop.js").AsTask());
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

        /// <summary>
        /// Initialise the game JS interop stuff
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="targetFramesPerSecond"></param>
        /// <returns></returns>
        public async ValueTask InitialiseGameAsync(object instance)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("initialiseGame", instance);
        }

        public async ValueTask InitialiseCanvasMouseHandlersAsync(ElementReference instance)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("addMouseCanvasHandlers", instance);
        }

        #endregion
    }
}
