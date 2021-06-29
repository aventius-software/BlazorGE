#region Namespaces

using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Core.Services
{
    internal class GameService : IAsyncDisposable
    {
        #region Private Properties

        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        #endregion

        #region Constructors

        public GameService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorGE.Core/interop.js").AsTask());
        }

        #endregion

        #region Public Methods        

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        /// <summary>
        /// Initialise the game JS interop stuff
        /// </summary>
        /// <param name="objectReference"></param>
        /// <param name="targetFramesPerSecond"></param>
        /// <returns></returns>
        public async ValueTask InitialiseGameAsync(object objectReference)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("initialiseGame", objectReference);
        }

        /// <summary>
        /// Set the desired target frames per second
        /// </summary>
        /// <param name="targetFramesPerSecond"></param>
        /// <returns></returns>
        public async ValueTask SetTargetFramesPerSecond(int targetFramesPerSecond)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setTargetFramesPerSecond", targetFramesPerSecond);
        }

        #endregion
    }
}
