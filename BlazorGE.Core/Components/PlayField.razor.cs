#region Namespaces

using BlazorGE.Core.Services;
using BlazorGE.Game;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

#endregion

namespace BlazorGE.Core.Components
{
    public class PlayFieldBase : ComponentBase, IAsyncDisposable
    {
        #region Parameters

        [Parameter]
        public bool Initialise2D { get; set; } = true;

        #endregion

        #region Injected Services

        [Inject]
        protected GameBase Game { get; set; }

        [Inject]
        private InternalGameInteropService GameService { get; set; }

        [Inject]
        private IGraphicsService2D GraphicsService2D { get; set; }

        [Inject]
        protected IKeyboardService KeyboardService { get; set; }

        [Inject]
        protected IMouseService MouseService { get; set; }

        #endregion

        #region Protected Fields

        protected GameTime GameTime;

        #endregion

        #region Override Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Only bother first time ;-)
            if (!firstRender) return;

            GraphicsService2D.OnInitialised += async (sender, args) =>
            {
                await GameService.InitialiseCanvasMouseHandlersAsync(args);
            };

            // Kick off the JS stuff            
            await GameService.InitialiseGameAsync(DotNetObjectReference.Create(this));

            // Initialise game            
            await Game.LoadContentAsync();
        }

        #endregion

        #region Implementations

        public async ValueTask DisposeAsync()
        {
            await Game.UnloadContentAsync();
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

        [JSInvokable]
        public async ValueTask OnMouseDown(double x, double y)
        {
            var state = MouseService.GetState();

            state.X = x;
            state.Y = y;
            state.KeyState = KeyState.Down;

            MouseService.SetState(state);

            await Task.CompletedTask;
        }

        [JSInvokable]
        public async ValueTask OnMouseMove(double x, double y)
        {
            var state = MouseService.GetState();

            state.X = x;
            state.Y = y;

            MouseService.SetState(state);

            await Task.CompletedTask;
        }

        [JSInvokable]
        public async ValueTask OnMouseUp(double x, double y)
        {
            var state = MouseService.GetState();

            state.X = x;
            state.Y = y;
            state.KeyState = KeyState.Up;

            MouseService.SetState(state);

            await Task.CompletedTask;
        }

        #endregion
    }
}
