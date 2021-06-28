#region Namespaces

using BlazorGE.Core.Services;
using BlazorGE.Game;
using BlazorGE.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Core.Components
{
    public class PlayFieldBase : ComponentBase
    {
        #region Injected Services

        [Inject]
        protected GameBase Game { get; set; }

        [Inject]
        protected GameService GameService { get; set; }

        [Inject]
        protected KeyboardService KeyboardService { get; set; }

        #endregion

        #region Protected Fields

        protected GameTime GameTime;

        #endregion

        #region Override Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Only bother first time ;-)
            if (!firstRender) return;

            // Kick off the JS stuff            
            await GameService.InitialiseGameAsync(DotNetObjectReference.Create(this));

            // Initialise game            
            await Game.LoadContentAsync();
        }

        #endregion

        #region JSInvokable Methods

        [JSInvokable]
        public async ValueTask GameLoop(float timeStamp, float timeDifference, float currentFramePerSecond)
        {
            // Update the game time
            GameTime.FramesPerSecond = currentFramePerSecond;
            GameTime.TimeSinceLastFrame = timeDifference;
            GameTime.TotalGameTime = timeStamp;

            // Run our games update/draw methods
            await Game.UpdateAsync(GameTime);
            await Game.DrawAsync(GameTime);
        }

        [JSInvokable]
        public async ValueTask OnKeyDown(int keyCode)
        {
            KeyboardService.GetState().SetKeyState((Keys)keyCode, KeyState.Down);

            await Task.CompletedTask;
        }

        [JSInvokable]
        public async ValueTask OnKeyUp(int keyCode)
        {
            KeyboardService.GetState().SetKeyState((Keys)keyCode, KeyState.Up);

            await Task.CompletedTask;
        }

        #endregion
    }
}
