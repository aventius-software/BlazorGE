#region Namespaces

using BlazorGE.Game;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using System.Threading.Tasks;

#endregion

namespace DemoGame.Game
{
    public class GameMain : GameBase
    {
        protected GraphicsService GraphicsService;        
        protected Keyboard Keyboard;
        protected SpriteSheet SpriteSheet;
        protected int X = 0, Y = 0;

        public GameMain(GraphicsService graphicsService, Keyboard keyboard)
        {
            GraphicsService = graphicsService;
            Keyboard = keyboard;
        }

        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.LeftArrow)) X--;
            else if (kstate.IsKeyDown(Keys.RightArrow)) X++;

            if (kstate.IsKeyDown(Keys.UpArrow)) Y--;
            else if (kstate.IsKeyDown(Keys.DownArrow)) Y++;

            await GraphicsService.ClearRectAsync(0, 0, GraphicsService.PlayFieldWidth, GraphicsService.PlayFieldHeight);
            await GraphicsService.DrawImageAsync(SpriteSheet.ElementReference, X, Y, 59, 59, 0, 0, 59, 59);
            await GraphicsService.DrawTextAsync("Hello there", 50, 50, "Arial", "#ff00ff", 30, true);

            await base.DrawAsync(gameTime);
        }

        public override async Task LoadContentAsync()
        {
            X = 0;
            Y = 0;

            SpriteSheet = GraphicsService.CreateSpriteSheet("images/car.png");

            await base.LoadContentAsync();
        }        
    }
}
