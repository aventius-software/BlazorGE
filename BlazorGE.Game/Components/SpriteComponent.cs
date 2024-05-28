#region Namespaces

using BlazorGE.Graphics2D;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public class SpriteComponent : GameComponentBase, IDrawableGameComponent, IUpdatableGameComponent
    {
        #region Protected Properties

        protected IGraphicsService2D GraphicsService;

        #endregion

        #region Public Properties

        public Sprite Sprite;

        #endregion

        #region Constructors

        public SpriteComponent(Sprite sprite, IGraphicsService2D graphicsService)
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
                var transformComponent = GameEntityOwner.GetComponent<Transform2DComponent>() ?? throw new Exception("No transform component found");
                Sprite.X = (int)transformComponent.Position.X;
                Sprite.Y = (int)transformComponent.Position.Y;
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
