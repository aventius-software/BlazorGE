#region Namespaces

using BlazorGE.Graphics.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Components
{
    public class GraphicAssetsBase : ComponentBase
    {
        #region Injected Services

        [Inject]
        protected IGraphicAssetService GraphicAssetService { get; set; }

        #endregion

        #region Protected Fields

        protected List<SpriteSheet> SpriteSheets = new();

        #endregion

        #region Override Methods

        protected override void OnInitialized()
        {
            GraphicAssetService.RegisterOnCreateSpriteSheetHandler(CreateSpriteSheet);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// When the specified asset has been loaded, this is called
        /// </summary>
        /// <param name="uniqueIdentifier"></param>
        /// <returns></returns>
        protected async Task OnLoadSpriteSheet(Guid uniqueIdentifier)
        {
            // Try and find this sprite sheet
            var spriteSheets = SpriteSheets.Where(sheet => sheet.UniqueIdentifier == uniqueIdentifier);

            // Does it exist? 
            if (spriteSheets.Any())
            {
                // Yes, get it...
                var spriteSheet = spriteSheets.Single();
                spriteSheet.IsLoaded = true;

                // Is there an 'onload' method to call?
                if (spriteSheet.OnLoadAsync != null)
                {
                    // Yes, call it and pass the current time of when the image was loaded
                    await InvokeAsync(async () => await spriteSheet.OnLoadAsync(DateTime.Now));
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a sprite sheet
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>        
        public SpriteSheet CreateSpriteSheet(string url)
        {
            // Might move to 'Core' project?
            var spriteSheet = new SpriteSheet { Url = url };
            SpriteSheets.Add(spriteSheet);
            StateHasChanged();

            return spriteSheet;
        }

        #endregion        
    }
}
