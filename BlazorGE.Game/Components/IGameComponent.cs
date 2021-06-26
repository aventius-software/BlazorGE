using BlazorGE.Game.Entities;

namespace BlazorGE.Game.Components
{
    public interface IGameComponent 
    {
        public GameEntityBase GameEntityOwner { get; set; }
    }
}
