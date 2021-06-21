using BlazorGE.Game;
using BlazorGE.Graphics;
using BlazorGE.Graphics.Services;
using BlazorGE.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGame.Game
{
    public class GameMain : GameBase
    {
        protected GraphicsService GraphicsService;
        protected SpriteSheet SpriteSheet;
        protected Keyboard Keyboard;
        protected int X = 0, Y = 0;
        public GameMain(GraphicsService graphicsService, Keyboard keyboard)
        {
            GraphicsService = graphicsService;
            Keyboard = keyboard;
        }

        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Left)) X--;
            else if (kstate.IsKeyDown(Keys.Right)) X++;

            await GraphicsService.ClearRectAsync(0, 0, GraphicsService.Width, GraphicsService.Height);
            await GraphicsService.DrawImageAsync(SpriteSheet.ElementReference, X, Y, 59, 59, 0, 0, 59, 59);

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
