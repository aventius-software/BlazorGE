#region Namespaces

using BlazorGE.Graphics.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

#endregion

namespace BlazorGE.Graphics.Components
{
    public class GraphicAssetsBase : ComponentBase
    {
        #region Injected Services

        [Inject]
        protected GraphicsService GraphicsService { get; set; }

        #endregion

        #region Protected Fields

        protected List<SpriteSheet> SpriteSheets = new List<SpriteSheet>();

        #endregion

        #region Override Methods

        protected override void OnInitialized()
        {
            GraphicsService.RegisterOnCreateSpriteSheetHandler(CreateSpriteSheet);
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
