#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Screens;
using BlazorGE.Graphics.Services;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoOutrunStyleGame.Game.Screens;
using System.Threading.Tasks;

#endregion

namespace DemoOutrunStyleGame.Game
{
    public class GameMain : GameBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Constructors

        public GameMain(GameWorld gameWorld, IGameScreenService gameScreenManager, IGraphicsService2D graphicsService, IGraphicAssetService graphicAssetService, IKeyboardService keyboardService)
        {
            GameWorld = gameWorld;
            GameScreenManager = gameScreenManager;
            GraphicsService = graphicsService;
            GraphicAssetService = graphicAssetService;
            KeyboardService = keyboardService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            // Start batching graphics calls
            await GraphicsService.BeginBatchAsync();

            // Clear screen and draw the game...
            await GraphicsService.ClearScreenAsync();
            await GameScreenManager.DrawAsync(gameTime);

            await base.DrawAsync(gameTime);

            // End/flush batched graphics calls
            await GraphicsService.EndBatchAsync();
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            // Create our screen and load it                      
            await GameScreenManager.LoadScreenAsync(new GamePlayScreen(GameWorld, GraphicsService, GraphicAssetService, KeyboardService));

            await base.LoadContentAsync();
        }

        /// <summary>
        /// Unload/dispose of any resources
        /// </summary>
        /// <returns></returns>
        public override async Task UnloadContentAsync()
        {
            // Unload/dispose of any resources
            await GameScreenManager.UnloadScreenAsync();

            await base.UnloadContentAsync();
        }

        /// <summary>
        /// Update the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Update the current game screen
            await GameScreenManager.UpdateAsync(gameTime);

            await base.UpdateAsync(gameTime);
        }

        #endregion
    }

    /*
    public struct Camera
    {
        public float x, y, distanceToPlayer;

        public float distanceToPlane
        {
            get
            {
                return 1 / (y / distanceToPlayer);
            }
        }

        public float z
        {
            get
            {
                return -1 * distanceToPlayer;
            }
        }
    }

    public struct WorldCoordinate
    {
        public float x, y, z;
    }

    public struct ScreenCoordinate
    {
        public int x, y, w;
    }

    public struct Point
    {
        public WorldCoordinate WorldCoordinates;
        public ScreenCoordinate ScreenCoordinates;
        public int Scale;

        public void Project2D(int screenWidth, int screenHeight, int roadWidth)
        {
            ScreenCoordinates.x = screenWidth / 2;
            ScreenCoordinates.y = (int)(screenHeight - WorldCoordinates.z);
            ScreenCoordinates.w = roadWidth;
        }

        public void Project3D(int screenWidth, int screenHeight, int roadWidth, float cameraX, float cameraY, float cameraZ, float cameraDistanceToScreen)
        {
            var transX = WorldCoordinates.x - cameraX;
            var transY = WorldCoordinates.y - cameraY;
            var transZ = WorldCoordinates.z - cameraZ;

            var scale = cameraDistanceToScreen / transZ;

            var projectX = scale * transX;
            var projectY = scale * transY;
            var projectW = scale * roadWidth;

            ScreenCoordinates.x = (int)Math.Round((1 + projectX) * (screenWidth / 2));
            ScreenCoordinates.y = (int)Math.Round((1 - projectY) * (screenHeight / 2));
            ScreenCoordinates.w = (int)Math.Round(projectW * (screenWidth / 2));
        }
    }

    public struct Segment
    {                
        public int Index;
        public Point Point;

        public Color Colour
        {
            get
            {
                return Math.Floor((float)Index / 3) % 2 == 1 ? Color.FromArgb(100, 100, 100) : Color.FromArgb(150, 150, 150);
            }
        }
    }
    */

    /*
    public class GameMain : GameBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;

        #endregion
        
        //int RoadWidth = 500;
        //int RoadLength;
        //int SegmentLength = 100;
        //int VisibleSegments = 50;                   

        //Camera Camera = new Camera { x = 0, y = 500, distanceToPlayer = 500 };
        //List<Segment> Segments = new List<Segment>();

        #region Constructors

        public GameMain(GameWorld gameWorld, IGameScreenService gameScreenManager, IGraphicsService2D graphicsService, IGraphicAssetService graphicAssetService, IKeyboardService keyboardService)
        {
            GameWorld = gameWorld;
            GameScreenManager = gameScreenManager;
            GraphicsService = graphicsService;
            GraphicAssetService = graphicAssetService;
            KeyboardService = keyboardService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            // Start batching graphics calls
            await GraphicsService.BeginBatchAsync();

            // Clear screen and draw the game...
            await GraphicsService.ClearScreenAsync();
            await GameScreenManager.DrawAsync(gameTime);

            // Call base method...
            await base.DrawAsync(gameTime);

            // End/flush batched graphics calls
            await GraphicsService.EndBatchAsync();
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            // Create our screen and load it                      
            await GameScreenManager.LoadScreenAsync(new GamePlayScreen(GameWorld, GraphicsService, GraphicAssetService, KeyboardService));

            await base.LoadContentAsync();
        }

        /// <summary>
        /// Unload/dispose of any resources
        /// </summary>
        /// <returns></returns>
        public override async Task UnloadContentAsync()
        {
            // Unload/dispose of any resources
            await GameScreenManager.UnloadScreenAsync();

            await base.UnloadContentAsync();
        }

        /// <summary>
        /// Update the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Update the current game screen
            await GameScreenManager.UpdateAsync(gameTime);

            await base.UpdateAsync(gameTime);
        }

        #endregion
    */

    /*
    public void CreateRoad()
    {
        Segments.AddRange(CreateSection(1000));
        RoadLength = Segments.Count * SegmentLength;
    }

    public IEnumerable<Segment> CreateSection(int numberOfSegments)
    {
        var segments = new List<Segment>();

        for(var i=0;i<numberOfSegments;i++)
        {
            segments.Add(CreateSegment(i, i));
        }

        return segments;
    }

    public Segment CreateSegment(int index, int totalSegments)
    {
        return new Segment
        {                
            Index = index,
            Point = new Point
            {
                Scale = -1,
                ScreenCoordinates = new ScreenCoordinate
                {
                    x = 0,
                    y = 0,
                    w = 0
                },
                WorldCoordinates = new WorldCoordinate
                {
                    x = 0,
                    y = 0,
                    z = index * 100
                }
            }
        };
    }

    public Segment GetSegment(float z)
    {
        if (z < 0) z += RoadLength;
        var index = (int)Math.Floor(z / SegmentLength) % Segments.Count;
        return Segments[index];
    }

    #region Override Methods

    /// <summary>
    /// Draw the game
    /// </summary>
    /// <param name="gameTime"></param>
    /// <returns></returns>
    public override async ValueTask DrawAsync(GameTime gameTime)
    {            
        await GraphicsService.BeginBatchAsync();

        // First clear the screen, then draw the current game screen
        await GraphicsService.ClearScreenAsync();

        var baseSegment = GetSegment(Camera.z);
        var baseIndex = baseSegment.Index;

        for (var n = 0; n < VisibleSegments; n++)
        {
            var currIndex = (baseIndex + n) % Segments.Count;
            var currSegment = Segments[currIndex];

            currSegment.Point.Project3D(GraphicsService.PlayFieldWidth, GraphicsService.PlayFieldHeight, RoadWidth, Camera.x, Camera.y, Camera.z, Camera.distanceToPlane);

            if (n > 0)
            {
                var prevIndex = (currIndex > 0) ? currIndex - 1 : Segments.Count - 1;
                var prevSegment = Segments[prevIndex];

                prevSegment.Point.Project3D(GraphicsService.PlayFieldWidth, GraphicsService.PlayFieldHeight, RoadWidth, Camera.x, Camera.y, Camera.z, Camera.distanceToPlane);

                await DrawSegment(n, prevSegment, currSegment);

                //var p1 = prevSegment.Point.ScreenCoordinates;
                //var p2 = currSegment.Point.ScreenCoordinates;

                //Color grass = (n / 3) % 2 == 1 ? Color.FromArgb(16, 200, 16) : Color.FromArgb(0, 154, 0);
                //Color rumble = (n / 3) % 2 == 1 ? Color.FromArgb(27, 27, 27) : Color.FromArgb(25, 25, 25);
                //Color road = (n / 3) % 2 == 1 ? Color.FromArgb(127, 127, 127) : Color.FromArgb(105, 105, 105);

                //await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(grass),
                //    0, p1.y, // x1, y1
                //    p1.x - p1.w, p1.y, // x2, y2
                //    p2.x - p2.w, p2.y, // x3, y3
                //    0, p2.y); // x4, y4

                //await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(grass),
                //    p1.x, p1.y, // x1, y1
                //    GraphicsService.PlayFieldWidth, p1.y, // x2, y2
                //    GraphicsService.PlayFieldWidth, p2.y, // x3, y3
                //    p2.x, p2.y); // x4, y4

                //await GraphicsService.DrawFilledRectangleAsync(ColorTranslator.ToHtml(grass), 0, p2.y, GraphicsService.PlayFieldWidth, p1.y - p2.y);


                //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(road), p1.x, p1.y, p1.w, p2.x, p2.y, p2.w);

                //await GraphicsService.dra(ColorTranslator.ToHtml(rumble), new int { p1.x, p1.y, p1.w, p2.x, p2.y, p2.w);
                //await DrawSegment(road, grass, p1.x, p1.y, p1.w, p2.x, p2.y, p2.w);
            }
        }

        await GraphicsService.DrawTextAsync($"FPS: {gameTime.FramesPerSecond}", 0, 30, "Arial", "red", 30, true);

        await GraphicsService.EndBatchAsync();
    }

    public async ValueTask DrawSegment(int n, Segment prevSegment, Segment currSegment, int numberOfLanes = 3)
    {
        Color grass = (n / 3) % 2 == 1 ? Color.FromArgb(16, 200, 16) : Color.FromArgb(0, 154, 0);
        Color rumble = (n / 3) % 2 == 1 ? Color.FromArgb(255, 255, 255) : Color.FromArgb(250, 0, 0);
        Color road = (n / 3) % 2 == 1 ? Color.FromArgb(127, 127, 127) : Color.FromArgb(105, 105, 105);
        Color lane = Color.FromArgb(255, 255, 255);

        var p1 = prevSegment.Point.ScreenCoordinates;
        var p2 = currSegment.Point.ScreenCoordinates;

        // Draw grass
        await GraphicsService.DrawFilledRectangleAsync(ColorTranslator.ToHtml(grass), 0, p2.y, GraphicsService.PlayFieldWidth, p1.y - p2.y);


        // Draw the road
        var x1 = p1.x - p1.w;
        var y1 = p1.y;
        var x2 = p1.x + p1.w;
        var y2 = p1.y;
        var x3 = p2.x + p2.w;
        var y3 = p2.y;
        var x4 = p2.x - p2.w;
        var y4 = p2.y;

        await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(road), x1, y1, x2, y2, x3, y3, x4, y4);

        var rx1 = x1 - (p1.w / 5);
        var ry1 = y1;
        var rx2 = x1;
        var ry2 = y2;
        var rx3 = x4;
        var ry3 = y3;
        var rx4 = x4 - (p2.w / 5);
        var ry4 = y4;

        await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(rumble), rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4);

        rx1 = x2;
        ry1 = y1;
        rx2 = x2 + (p1.w / 5);
        ry2 = y2;
        rx3 = x3 + (p2.w / 5);
        ry3 = y3;
        rx4 = x3;
        ry4 = y4;

        await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(rumble), rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4); 

        if (true)//rumble == Color.FromArgb(255, 255, 255))
        {
            var line_w1 = (p1.w / 20) / 2;
            var line_w2 = (p2.w / 20) / 2;

            var lane_w1 = (p1.w * 2) / numberOfLanes;
            var lane_w2 = (p2.w * 2) / numberOfLanes;

            var lane_x1 = x1 - p1.w;
            var lane_x2 = x2 - p2.w;

            for(var i = 1; i < numberOfLanes; i++)
            {
                lane_x1 += lane_w1;
                lane_x2 += lane_w2;

                await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(rumble),
                    lane_x1 - line_w1, y1,
                    lane_x1 + line_w1, y1,
                    lane_x2 + line_w2, y2,
                    lane_x2 - line_w2, y2
                );
            }
        }
    }
    */

    //public override async Task LoadContentAsync()
    //{
    //RoadWidth = 250;
    //CreateRoad();

    //await base.LoadContentAsync();
    //}

    //#endregion
}

    /*
    public struct Segment
    {        
        public float WorldX, WorldY, WorldZ; // 3d centre of the line of a segment
        public float X, Y, W; // centre of the line of a segment (screen coordinate)
        public float Scale;

        public void ProjectWorldToScreenCoordinates(            
            int screenWidth, 
            int screenHeight, 
            int roadWidth, 
            float cameraDepth, 
            int cameraX, 
            int cameraY, 
            int cameraZ)
        {
            Scale = cameraDepth / (WorldZ - cameraZ);
            X = (1 + Scale * (WorldX - cameraX)) * screenWidth / 2;
            Y = (1 - Scale * (WorldY - cameraY)) * screenHeight / 2;
            W = Scale * roadWidth * screenWidth / 2;
        }
    }

    public class GameMain : GameBase
    {
        #region Protected Properties

        protected IGameScreenService GameScreenManager;
        protected GameWorld GameWorld;
        protected IGraphicAssetService GraphicAssetService;
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;

        #endregion

        float CameraDepth = 0.84f; // camera depth
        int RoadWidth = 2000;
        int SegmentSize = 200; // segment length (from bottom to top)        
        Segment[] Segments = new Segment[1600];        

        #region Constructors

        public GameMain(GameWorld gameWorld, IGameScreenService gameScreenManager, IGraphicsService2D graphicsService, IGraphicAssetService graphicAssetService, IKeyboardService keyboardService)
        {
            GameWorld = gameWorld;
            GameScreenManager = gameScreenManager;
            GraphicsService = graphicsService;
            GraphicAssetService = graphicAssetService;
            KeyboardService = keyboardService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask DrawAsync(GameTime gameTime)
        {
            // First clear the screen, then draw the current game screen
            await GraphicsService.ClearScreenAsync();
            
            var N = Segments.Length;

            for (var n = 0; n < 100; n++)
            {
                var l = Segments[n % N];

                //l.ProjectWorldToScreenCoordinates(
                //    GraphicsService.PlayFieldWidth,
                //    GraphicsService.PlayFieldHeight,
                //    RoadWidth,
                //    CameraDepth,
                //    0, 1500, 0);

                //Segments[n % N] = l;

                if (n > 0)
                {
                    Segment p = Segments[(n - 1) % N];

                    Color grass = (n / 3) % 2 == 1 ? Color.FromArgb(16, 200, 16) : Color.FromArgb(0, 154, 0);
                    Color rumble = (n / 3) % 2 == 1 ? Color.FromArgb(27, 27, 27) : Color.FromArgb(25, 25, 25);
                    Color road = (n / 3) % 2 == 1 ? Color.FromArgb(107, 107, 107) : Color.FromArgb(105, 105, 105);

                    await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(grass), 0, (int)p.Y, GraphicsService.PlayFieldWidth, 0, (int)l.Y, GraphicsService.PlayFieldWidth);
                    //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(rumble), (int)p.X, (int)p.Y, (int)(p.W * 1.2), (int)l.X, (int)l.Y, (int)(l.W * 1.2));
                    //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(road), p.X, p.Y, p.W, l.X, l.Y, l.W);
                }
            }

            await GraphicsService.DrawTextAsync($"FPS: {gameTime.FramesPerSecond}", 0, 30, "Arial", "red", 30, true);

            await base.DrawAsync(gameTime);
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <returns></returns>
        public override async Task LoadContentAsync()
        {
            Segments = BuildRoadGeometry(1600);
            //var segments = new List<Segment>();

            //for(int numberOfSegments = 0; numberOfSegments < 1600; numberOfSegments++)
            //{
            //    Segment segment = new() { WorldX = 0, WorldY = 0, WorldZ = 0, Scale = 0, W = 0, X = 0, Y = 0 };
            //    segment.WorldZ = numberOfSegments * SegmentSize;
            //    segments.Add(segment);
            //}

            //Segments = segments.ToArray();

            //var N = Segments.Length;

            //for (var n = 0; n < 300; n++)
            //{
            //    var l = Segments[n % N];

            //    //l.ProjectWorldToScreenCoordinates(
            //    //    GraphicsService.PlayFieldWidth,
            //    //    GraphicsService.PlayFieldHeight,
            //    //    RoadWidth,
            //    //    CameraDepth,
            //    //    0, 1500, 0);

            //    //Segments[n % N] = l;

            //    if (n > 0)
            //    {
            //        Segment p = Segments[(n - 1) % N];

            //        Color grass = (n / 3) % 2 == 1 ? Color.FromArgb(16, 200, 16) : Color.FromArgb(0, 154, 0);
            //        Color rumble = (n / 3) % 2 == 1 ? Color.FromArgb(27, 27, 27) : Color.FromArgb(25, 25, 25);
            //        Color road = (n / 3) % 2 == 1 ? Color.FromArgb(107, 107, 107) : Color.FromArgb(105, 105, 105);

            //        await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(grass), 0, (int)p.Y, GraphicsService.PlayFieldWidth, 0, (int)l.Y, GraphicsService.PlayFieldWidth);
            //        //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(rumble), p.X, p.Y, (int)(p.W * 1.2), l.X, l.Y, (int)(l.W * 1.2));
            //        //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(road), p.X, p.Y, p.W, l.X, l.Y, l.W);
            //    }
            //}

            await base.LoadContentAsync();
        }

        /// <summary>
        /// Unload/dispose of any resources
        /// </summary>
        /// <returns></returns>
        public override async Task UnloadContentAsync()
        {
            // Unload/dispose of any resources
            //await GameScreenManager.UnloadScreenAsync();

            await base.UnloadContentAsync();
        }

        /// <summary>
        /// Update the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override async ValueTask UpdateAsync(GameTime gameTime)
        {
            // Update the current game screen
            //await GameScreenManager.UpdateAsync(gameTime);

            await base.UpdateAsync(gameTime);
        }

        #endregion

        private Segment[] BuildRoadGeometry(int numberOfSegments)
        {
            var segments = new List<Segment>();

            for (int i = 0; i < numberOfSegments; i++)
            {
                Segment segment = new() { WorldX = 0, WorldY = 0, WorldZ = 0, Scale = 0, W = 0, X = 0, Y = 0 };
                segment.WorldZ = i * SegmentSize;
                segments.Add(segment);
            }

            var N = segments.Count;//.Length;

            for (var n = 0; n < 300; n++)
            {
                var l = segments[n % N];

                l.ProjectWorldToScreenCoordinates(
                    GraphicsService.PlayFieldWidth,
                    GraphicsService.PlayFieldHeight,
                    RoadWidth,
                    CameraDepth,
                    0, 500, 0);

                segments[n % N] = l;
            }

            return segments.ToArray();

            //    if (n > 0)
            //    {
            //        Segment p = Segments[(n - 1) % N];

            //        Color grass = (n / 3) % 2 == 1 ? Color.FromArgb(16, 200, 16) : Color.FromArgb(0, 154, 0);
            //        Color rumble = (n / 3) % 2 == 1 ? Color.FromArgb(27, 27, 27) : Color.FromArgb(25, 25, 25);
            //        Color road = (n / 3) % 2 == 1 ? Color.FromArgb(107, 107, 107) : Color.FromArgb(105, 105, 105);

            //        await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(grass), 0, p.Y, GraphicsService.PlayFieldWidth, 0, l.Y, GraphicsService.PlayFieldWidth);
            //        //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(rumble), p.X, p.Y, (int)(p.W * 1.2), l.X, l.Y, (int)(l.W * 1.2));
            //        //await GraphicsService.DrawTrapeziumAsync(ColorTranslator.ToHtml(road), p.X, p.Y, p.W, l.X, l.Y, l.W);
            //    }
            //}
        }
    }*/
//}
