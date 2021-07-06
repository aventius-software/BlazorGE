#region Namespaces

using BlazorGE.Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Entities
{
    public class GameEntity
    {
        #region Protected Properties

        protected List<IGameComponent> GameComponents = new();

        #endregion

        #region Public Properties
        
        public List<GameEntity> Children { get; protected set; } = new();
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsActive { get; protected set; }
        public GameEntity Parent { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set this entity as active
        /// </summary>
        public void Activate() => IsActive = true;

        /// <summary>
        /// Create and add a child entity to this entity
        /// </summary>
        /// <returns></returns>
        public GameEntity AddChild()
        {
            var child = new GameEntity
            {
                Parent = this
            };

            Children.Add(child);

            return child;
        }

        /// <summary>
        /// Attach the specified game component to this entity
        /// </summary>
        /// <param name="gameComponent"></param>
        public void AttachGameComponent(IGameComponent gameComponent)
        {
            // Add component if it doesn't already exist!
            if (!GameComponents.Any(component => component.GetType() == gameComponent.GetType()))
            {
                gameComponent.GameEntityOwner = this;
                GameComponents.Add(gameComponent);
            }
        }

        /// <summary>
        /// Set this entity and any of its children to inactive
        /// </summary>
        /// <param name="deactivateChildren"></param>
        public void Deactivate(bool deactivateChildren = true)
        {
            IsActive = false;

            if (deactivateChildren)
            {
                foreach (var childEntity in Children.Where(entity => entity.IsActive))
                {
                    childEntity.Deactivate();
                }
            }
        }

        /// <summary>
        /// Remove all attached game components of specified type from this entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DetachGameComponent<T>() where T : IGameComponent
        {
            GameComponents.RemoveAll(component => component is T);
        }

        /// <summary>
        /// Draw this entity
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async Task DrawAsync(GameTime gameTime)
        {
            foreach (var gameComponent in GameComponents.Where(component => component is IDrawableGameComponent))
            {
                await ((IDrawableGameComponent)gameComponent).DrawAsync(gameTime);
            }

            foreach (var childEntity in Children.Where(entity => entity.IsActive))
            {
                await childEntity.DrawAsync(gameTime);
            }
        }

        /// <summary>
        /// Get the attached game component, throws exception if none found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : IGameComponent
        {
            return (T)GameComponents.Where(component => component is T).Single();
        }

        /// <summary>
        /// Returns true if this entity has an attached game component of the specified type
        /// </summary>
        /// <typeparam name="T">Type of game component</typeparam>
        /// <returns></returns>
        public bool HasComponent<T>() where T : IGameComponent
        {
            return GameComponents.Any(component => component is T);
        }

        /// <summary>
        /// Update this entity
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(GameTime gameTime)
        {
            foreach (var gameComponent in GameComponents.Where(component => component is IUpdatableGameComponent))
            {
                await ((IUpdatableGameComponent)gameComponent).UpdateAsync(gameTime);
            }

            foreach (var childEntity in Children.Where(entity => entity.IsActive))
            {
                await childEntity.UpdateAsync(gameTime);
            }
        }

        #endregion
    }
}
