#region Namespaces

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Assets
{
    public class GraphicAsset
    {        
        public ElementReference ImageElementReference;
        public bool IsLoaded = false;
        public Func<DateTime, Task> OnLoadAsync = default!;
        public Guid UniqueIdentifier = Guid.NewGuid();
        public string Url = default!;
    }
}
