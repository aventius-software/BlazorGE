#region Namespaces

using BlazorGE.Game;
using BlazorGE.Input;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

#endregion

namespace BlazorGE.Core.Components
{
    [SupportedOSPlatform("browser")]
    public partial class PlayField : IAsyncDisposable
    {
        #region Parameters

        [Parameter]
        public bool Initialise2D { get; set; } = true;

        #endregion

        #region Injected Services

        [Inject]
        private GameBase Game { get; set; } = default!;

        [Inject]
        private IKeyboardService KeyboardService { get; set; } = default!;
        
        #endregion

        #region Private Fields

        private GameTime GameTime;
        private static PlayField Self = default!;

        #endregion

        #region Override Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Only bother first time ;-)
            if (!firstRender) return;

            // Load the JS module
            await JSHost.ImportAsync("playField", $"/_content/BlazorGE.Core/playFieldInterop.js");

            // Initialise some stuff on the JS side
            InitialiseModule("BlazorGE.Core");

            // Save a static reference to this object
            Self = this;

            // Initialise game            
            await Game.LoadContentAsync();
        }

        #endregion

        #region IAsyncDisposable Implementation

        public async ValueTask DisposeAsync()
        {
            await Game.UnloadContentAsync();
        }

        #endregion

        #region JS Import Interop Methods

        [JSImport("initialiseModule", "playField")]
        internal static partial void InitialiseModule([JSMarshalAs<JSType.String>] string assemblyName);

        #endregion

        #region JS Export Interop Methods

        /// <summary>
        /// Called by JS every animation frame
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="timeDifference"></param>
        /// <param name="currentFramePerSecond"></param>
        [JSExport]
        internal static void GameLoop(float timeStamp, float timeDifference, float currentFramePerSecond)
        {
            // Update the game time
            Self.GameTime.FramesPerSecond = currentFramePerSecond;
            Self.GameTime.TimeSinceLastFrame = timeDifference;
            Self.GameTime.TotalGameTime = timeStamp;

            // Run our games update/draw methods
            Task.Run(async () =>
            {
                await Self.Game.UpdateAsync(Self.GameTime);
                await Self.Game.DrawAsync(Self.GameTime);
            });
        }

        /// <summary>
        /// Called by JS if key down event fires
        /// </summary>
        /// <param name="keyCode"></param>
        [JSExport]
        internal static void OnKeyDown(int keyCode)
        {
            Self.KeyboardService.GetState().SetKeyState((Keys)keyCode, KeyState.Down);
        }

        /// <summary>
        /// Called by JS if key up event fires
        /// </summary>
        /// <param name="keyCode"></param>
        [JSExport]
        internal static void OnKeyUp(int keyCode)
        {
            Self.KeyboardService.GetState().SetKeyState((Keys)keyCode, KeyState.Up);
        }        

        #endregion
    }
}
