#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Systems;
using BlazorGE.Graphics2D.Services;
using System.Threading.Tasks;

#endregion

namespace DemoPongStyleGame.Game.Systems
{
    public class ArenaSystem : IGameDrawSystem
    {
        protected const int LineBlockHeight = 50;
        protected const int LineBlockWidth = 50;

        protected IGraphicsService2D GraphicsService2D;

        public ArenaSystem(IGraphicsService2D graphicsService2D)
        {
            GraphicsService2D = graphicsService2D;
        }

        public async ValueTask DrawAsync(GameTime gameTime)
        {
            var x = (GraphicsService2D.CanvasWidth / 2) - (LineBlockWidth / 2);

            for (var i = 0; i <= GraphicsService2D.CanvasHeight / (LineBlockHeight * 2); i++)
            {
                await GraphicsService2D.DrawFilledRectangleAsync("white", x, i * (LineBlockHeight * 2), LineBlockWidth, LineBlockHeight);
            }
        }
    }
}
