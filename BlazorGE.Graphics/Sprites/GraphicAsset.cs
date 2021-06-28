#region Namespaces

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

#endregion

namespace BlazorGE.Graphics.Sprites
{
    public class GraphicAsset
    {        
        public ElementReference ImageElementReference;
        public bool IsLoaded = false;
        public Func<DateTime, Task> OnLoadAsync;
        public Guid UniqueIdentifier = Guid.NewGuid();
        public string Url;
    }
}
