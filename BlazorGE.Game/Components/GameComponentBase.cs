﻿#region Namespaces

using BlazorGE.Game.Entities;

#endregion

namespace BlazorGE.Game.Components
{
    public class GameComponentBase : IGameComponent
    {
        /// <summary>
        /// The entity who owns this component
        /// </summary>
        public GameEntity GameEntityOwner { get; set; } = default!;
    }
}
