#region Namespaces

using BlazorGE.Graphics.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Components
{
    public class CanvasBase : ComponentBase
    {
        #region Injected Services

        [Inject]
        protected IGraphicsService2D GraphicsService { get; set; }

        #endregion

        #region Protected Properties

        protected ElementReference CanvasReference;

        #endregion

        #region Override Methods

        protected override async Task OnInitializedAsync()
        {
            await GraphicsService.InitialiseCanvas2D();
        }

        #endregion
    }
}
