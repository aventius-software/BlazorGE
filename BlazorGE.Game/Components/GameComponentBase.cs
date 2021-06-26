#region Namespaces

using BlazorGE.Game.Entities;

#endregion

namespace BlazorGE.Game.Components
{
    public class GameComponentBase : IGameComponent
    {
        public GameEntityBase GameEntityOwner { get; set; }
    }
}
