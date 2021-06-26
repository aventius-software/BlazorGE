#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Screens
{
    public interface IGameScreen
    {
        public ValueTask DrawAsync(GameTime gameTime);
        public void LoadContent();
        public Task LoadContentAsync();
        public void UnloadContent();
        public Task UnloadContentAsync();
        public ValueTask UpdateAsync(GameTime gameTime);
    }
}
