#region Namespaces

using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Core.Services
{
    public class CoreInteropService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public CoreInteropService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorGE.Core/interop.js").AsTask());
        }

        public async ValueTask InitialiseGameAsync(object objectReference)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("initialiseGame", objectReference);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
