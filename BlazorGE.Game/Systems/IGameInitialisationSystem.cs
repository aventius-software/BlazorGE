using System.Threading.Tasks;

namespace BlazorGE.Game.Systems
{
    public interface IGameInitialisationSystem
    {
        public Task InitialiseAsync();
    }
}
