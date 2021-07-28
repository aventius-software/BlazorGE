#region Namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Services
{
    public class GraphicBatchingService
    {
        #region Protected Properties

        protected List<object[]> BatchItems = new();
        protected bool IsBatching = false;

        #endregion

        #region Public Methods

        public async ValueTask BatchCallAsync(params object[] parameters)
        {
            BatchItems.Add(parameters);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Start batching graphics calls
        /// </summary>
        /// <returns></returns>
        public async ValueTask BeginBatchingCallsAsync()
        {
            if (!IsBatching)
            {
                BatchItems.Clear();
                IsBatching = true;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// End (or flush) all batched graphics calls
        /// </summary>
        /// <returns></returns>
        public async ValueTask<object[]> GetBatchedCallsAsync()
        {
            if (IsBatching)
            {
                IsBatching = false;
            }

            await Task.CompletedTask;

            return BatchItems.ToArray();
        }

        #endregion
    }
}
