#region Namespaces

using BlazorGE.Graphics.Assets;

#endregion

namespace BlazorGE.Graphics2D
{
    public struct Sprite(GraphicAsset spriteSheet, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int width, int height, int x = 0, int y = 0)
    {
        public int Height = height;
        public int SourceHeight = sourceHeight;
        public int SourceWidth = sourceWidth;
        public int SourceX = sourceX;
        public int SourceY = sourceY;
        public GraphicAsset SpriteSheet = spriteSheet;
        public int Width = width;
        public int X = x;
        public int Y = y;
    }
}
