#region Namespaces

using BlazorGE.Game.Entities;

#endregion

namespace BlazorGE.Game.Components
{
    public interface IGameComponent
    {
        public GameEntity GameEntityOwner { get; set; }
    }
}
