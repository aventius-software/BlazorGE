#region Namespaces

using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public class SpriteComponent : GameComponentBase, IDrawableGameComponent, IUpdatableGameComponent
    {
        #region Protected Properties

        protected GraphicsService GraphicsService;

        #endregion

        #region Public Properties

        public Sprite Sprite;

        #endregion

        #region Constructors

        public SpriteComponent(Sprite sprite, GraphicsService graphicsService)
        {
            Sprite = sprite;
            GraphicsService = graphicsService;
        }

        #endregion

        #region Implementations

        public async ValueTask DrawAsync(GameTime gameTime)
        {
            await GraphicsService.DrawSpriteAsync(Sprite);
        }

        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Does the parent entity have a transform component?
            if (GameEntityOwner.HasComponent<Transform2DComponent>())
            {
                // Yes, ok lets update sprite with new position from the transform component
                var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>();
                Sprite.X = (int)transformComponent.Position.X;
                Sprite.Y = (int)transformComponent.Position.Y;
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
