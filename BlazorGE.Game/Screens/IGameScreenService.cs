#region Namespaces

using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Screens
{
    public interface IGameScreenService
    {
        public ValueTask DrawAsync(GameTime gameTime);
        public Task LoadScreenAsync(IGameScreen gameScreen);
        public Task UnloadScreenAsync();
        public ValueTask UpdateAsync(GameTime gameTime);
    }
}
