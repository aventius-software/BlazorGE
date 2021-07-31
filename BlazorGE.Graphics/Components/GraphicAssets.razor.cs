#region Namespaces

using BlazorGE.Graphics.Assets;
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

        protected List<GraphicAsset> Assets = new();

        #endregion

        #region Override Methods

        protected override void OnInitialized()
        {
            GraphicAssetService.RegisterOnCreateGraphicAssetHandler(CreateGraphicAsset);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// When the specified asset has been loaded, this is called
        /// </summary>
        /// <param name="uniqueIdentifier"></param>
        /// <returns></returns>
        protected async Task OnLoadGraphicAsset(Guid uniqueIdentifier)
        {
            // Try and find this sprite sheet
            var assets = Assets.Where(asset => asset.UniqueIdentifier == uniqueIdentifier);

            // Does it exist? 
            if (assets.Any())
            {
                // Yes, get it...
                var asset = assets.Single();
                asset.IsLoaded = true;

                // Is there an 'onload' method to call?
                if (asset.OnLoadAsync is not null)
                {
                    // Yes, call it and pass the current time of when the image was loaded
                    await InvokeAsync(async () => await asset.OnLoadAsync(DateTime.Now));
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a graphic asset
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>        
        public GraphicAsset CreateGraphicAsset(string url)
        {
            var asset = new GraphicAsset { Url = url };
            Assets.Add(asset);
            StateHasChanged();

            return asset;
        }

        #endregion        
    }
}
