namespace BlazorGE.Graphics
{
    public struct Sprite
    {
        #region Public Properties

        public int Height;
        public int SourceX;
        public int SourceY;
        public SpriteSheet SpriteSheet;
        public int Width;
        public int X;
        public int Y;

        #endregion

        #region Constructors

        public Sprite(SpriteSheet spriteSheet, int sourceX, int sourceY, int width, int height, int x = 0, int y = 0)
        {
            SpriteSheet = spriteSheet;
            SourceX = sourceX;
            SourceY = sourceY;
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        #endregion
    }
}
