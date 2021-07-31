#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Screens
{
    public interface IGameScreen
    {
        public ValueTask DrawAsync(GameTime gameTime);        
        public Task LoadContentAsync();        
        public Task UnloadContentAsync();
        public ValueTask UpdateAsync(GameTime gameTime);
    }
}
