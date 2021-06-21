#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game
{
    public class GameBase
    {
        public virtual async ValueTask DrawAsync(GameTime gameTime)
        {
            await Task.CompletedTask;
        }

        public virtual async Task LoadContentAsync()
        {
            await Task.CompletedTask;
        }

        public virtual async ValueTask UpdateAsync(GameTime gameTime)
        {
            await Task.CompletedTask;
        }        
    }
}
