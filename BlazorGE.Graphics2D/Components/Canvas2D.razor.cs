#region Namespaces

using BlazorGE.Graphics2D.Services;
using Microsoft.AspNetCore.Components;

#endregion

namespace BlazorGE.Graphics2D.Components
{
    public class Canvas2DBase : ComponentBase
    {
        #region Injected Services

        [Inject]
        protected IGraphicsService2D GraphicsService { get; set; }

        #endregion

        #region Protected Properties

        protected ElementReference CanvasReference;        

        #endregion 

        #region Override Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            if (!firstRender) return;
            
            await GraphicsService.InitialiseCanvasAsync(CanvasReference);            
            await CanvasReference.FocusAsync();
        }

        #endregion
    }
}
