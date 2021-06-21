#region Namespaces

using BlazorGE.Graphics.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var spriteSheet = new SpriteSheet { Url = url };
            SpriteSheets.Add(spriteSheet);
            StateHasChanged();

            return spriteSheet;
        }        

        #endregion
    }
}
