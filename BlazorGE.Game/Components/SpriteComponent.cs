#region Namespaces

using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Game.Components
{
    public class SpriteComponent : IDrawableGameComponent
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

        #region Interface Implementations

        public async ValueTask DrawAsync(GameTime gameTime)
        {
            await GraphicsService.DrawSpriteAsync(Sprite);
        }

        #endregion
    }
}
