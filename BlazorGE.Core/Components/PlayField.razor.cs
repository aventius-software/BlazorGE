#region Namespaces

using BlazorGE.Core.Services;
using BlazorGE.Game;
using BlazorGE.Graphics;
using BlazorGE.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Core.Components
{
    public class PlayFieldBase : ComponentBase
    {
        #region Injected Services

        [Inject]
        protected CoreInteropService CoreInteropService { get; set; }

        [Inject]
        protected GameBase Game { get; set; }

        [Inject]
        protected Keyboard Keyboard { get; set; }

        #endregion

        #region Protected Fields

        protected GameTime GameTime;
        protected DateTime PreviousTime = DateTime.Now;
        protected List<SpriteSheet> SpriteSheets = new List<SpriteSheet>();

        #endregion

        #region Override Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Only bother first time ;-)
            if (!firstRender) return;

            // Kick off the JS stuff            
            await CoreInteropService.InitialiseGameAsync(DotNetObjectReference.Create(this), 60);

            // Initialise game            
            await Game.LoadContentAsync();            
        }

        #endregion

        #region JSInvokable Methods

        [JSInvokable]
        public async ValueTask GameLoop(float timeStamp, float timeDifference, float currentFramePerSecond)
        {
            // Update the game time
            GameTime.FramesPerSecond = currentFramePerSecond;// 1.0 / (DateTime.Now - PreviousTime).TotalSeconds;
            GameTime.TimeSinceLastFrame = timeDifference;// GameTime.ElapsedGameTime - timeStamp;
            GameTime.TotalGameTime = timeStamp;

            // Run our games update/draw methods
            await Game.UpdateAsync(GameTime);
            await Game.DrawAsync(GameTime);
        }

        [JSInvokable]
        public async ValueTask OnKeyDown(int keyCode)
        {
            Keyboard.GetState().SetKeyState((Keys)keyCode, KeyState.Down);

            await Task.CompletedTask;
        }

        [JSInvokable]
        public async ValueTask OnKeyUp(int keyCode)
        {
            Keyboard.GetState().SetKeyState((Keys)keyCode, KeyState.Up);

            await Task.CompletedTask;
        }

        #endregion
    }
}
