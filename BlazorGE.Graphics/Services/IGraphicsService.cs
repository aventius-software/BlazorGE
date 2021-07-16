#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Services
{
    public interface IGraphicsService
    {
        public ValueTask BeginBatchAsync();
        public ValueTask EndBatchAsync();
    }
}
