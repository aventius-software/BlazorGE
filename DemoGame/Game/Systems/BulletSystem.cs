#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Game.Systems;
using DemoGame.Game.Components;

#endregion

namespace DemoGame.Game.Systems
{
    public class BulletSystem : GameEntityDrawAndUpdateSystemBase
    {
        #region Protected Properties

        protected GameWorld GameWorld;

        #endregion

        #region Constructors

        public BulletSystem(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// This system should only act upon the bullet entities
        /// </summary>
        /// <returns></returns>
        public override Func<GameEntity, bool> EntityPredicate()
        {
            return entity => entity.HasComponent<BulletMovementComponent>();
        }

        /// <summary>
        /// Update all the entities
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="filteredGameEntities"></param>
        /// <returns></returns>
        public override async ValueTask UpdateEntitiesAsync(GameTime gameTime, IEnumerable<GameEntity> filteredGameEntities)
        {
            // Only bother if there are any bullets active...
            if (filteredGameEntities.Any())
            {
                // Find enemy entities
                var enemies = GameWorld.GameEntities.Where(entity => entity.HasComponent<EnemyMovementComponent>() && entity.IsActive);

                // Now, for each bullet...
                for (var i = 0; i < filteredGameEntities.Count(); i++)
                {
                    var bullet = filteredGameEntities.ElementAt(i);

                    // ...first get the bullets transform
                    var bulletTransform = bullet.GetComponent<Transform2DComponent>();

                    // Destroy the bullet if its off screen!
                    if (bulletTransform.Position.Y < 0)
                    {
                        GameWorld.DestroyGameEntity(bullet);
                    }
                    else
                    {
                        // If its still moving on screen, check if its hit any enemies
                        foreach (var enemy in enemies)
                        {
                            if (bulletTransform.IntersectsWith(enemy.GetComponent<Transform2DComponent>()))
                            {
                                GameWorld.DestroyGameEntity(enemy);
                                GameWorld.DestroyGameEntity(bullet);
                            }
                        }
                    }
                }
            }

            await base.UpdateEntitiesAsync(gameTime, filteredGameEntities);
        }

        #endregion
    }
}
