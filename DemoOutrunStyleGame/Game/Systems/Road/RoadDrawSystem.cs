#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Systems;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace DemoOutrunStyleGame.Game.Systems.Road
{
    /// <summary>
    /// (a rough) Psuedo 3D Outrun style road rendering engine
    /// 
    /// Influenced by (and some code taken/translated) from the following, so credit where credit is due. Well
    /// worth checking out for improving and extending this basic version:-
    /// http://www.extentofthejam.com/pseudo/
    /// https://github.com/ssusnic/Pseudo-3d-Racer
    /// https://www.youtube.com/watch?v=N60lBZDEwJ8&list=PLB_ibvUSN7mzUffhiay5g5GUHyJRO4DYr&index=8
    /// https://codeincomplete.com/articles/javascript-racer-v1-straight/
    /// </summary>
    public class RoadDrawSystem : IGameDrawSystem, IGameUpdateSystem, IGameInitialisationSystem
    {
        #region Protected Properties

        protected Camera Camera; // = new();// { X = 0, Y = 500, DistanceToPlayer = 500 }; // The camera ;-)
        protected IGraphicsService2D GraphicsService; // Graphics service which handles rendering on canvas
        protected Color GrassColourDark = Color.FromArgb(16, 200, 16);
        protected Color GrassColourLight = Color.FromArgb(0, 154, 0);
        protected Color RoadColourDark = Color.FromArgb(127, 127, 127);
        protected Color RoadColourLight = Color.FromArgb(105, 105, 105);
        protected Color RumbleColourDark = Color.FromArgb(250, 0, 0);
        protected Color RumbleColourLight = Color.FromArgb(255, 255, 255);
        protected Color LaneColour = Color.FromArgb(255, 255, 255);
        protected int IndividualRoadSegmentLength = 100; // distance from top to bottom of a single road segment
        protected IKeyboardService KeyboardService; // Handling keyboard interaction
        protected int RoadLanes = 3; // Number of lanes on the road
        protected int RoadLength; // Total length of the road (i.e. number of segments * segment length)
        protected int RoadPosition = 500;
        protected List<RoadSegment> RoadSegments = new(); // A list of all the road segments
        protected int TotalRoadSegmentCount; // Total number of road segments in the road        
        protected int RoadWidth = 1000; // Half the width of the road
        protected int RumbleSegments = 5; // Number of segments that form a rumble strip
        protected int VisibleSegments = 200; // Number of road segments to render on screen at once

        #endregion

        #region Constructors

        public RoadDrawSystem(IGraphicsService2D graphicsService, IKeyboardService keyboardService)
        {
            GraphicsService = graphicsService;
            KeyboardService = keyboardService;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Draws the road
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public async ValueTask DrawAsync(GameTime gameTime)
        {
            // Get our current Z position for the camera
            var baseSegment = GetRoadSegment(Camera.Z);
            var baseIndex = baseSegment.Index;

            // Define the clipping bottom line to render only segments above it
            var clipBottomLine = GraphicsService.PlayFieldHeight;
            
            ProjectWorldToScreen(baseIndex, VisibleSegments, 0);

            // Draw the specified number of visible road segments on screen
            for (var roadSegment = 0; roadSegment < VisibleSegments; roadSegment++)
            {
                // Get the current road segment
                var currentRoadSegmentIndex = (baseIndex + roadSegment) % TotalRoadSegmentCount;
                var currentRoadSegment = RoadSegments[currentRoadSegmentIndex];

                // get the camera offset-Z to loop back the road
                var offsetZ = (currentRoadSegmentIndex < baseIndex) ? RoadLength : 0;

                // project the segment to the screen space
               // var projectedPoint = Project3D(currentRoadSegment.Point, Camera.X, Camera.Y, Camera.Z - offsetZ, Camera.DistanceToProjectionPlane);
                //currentRoadSegment.Point = projectedPoint;
                //RoadSegments[currentRoadSegmentIndex] = currentRoadSegment;

                // Draw this segment only if it is above the clipping bottom line
                var currBottomLine = currentRoadSegment.Point.ScreenCoordinates.Y;

                // If we're at the first we don't have a 'previous' segment, so skip...
                if (roadSegment > 0 && currBottomLine < clipBottomLine)
                {
                    // Get the previous road segment
                    var previousRoadSegmentIndex = currentRoadSegmentIndex > 0 ? currentRoadSegmentIndex - 1 : TotalRoadSegmentCount - 1;
                    var previousRoadSegment = RoadSegments[previousRoadSegmentIndex];

                    var p1 = previousRoadSegment.Point.ScreenCoordinates;
                    var p2 = currentRoadSegment.Point.ScreenCoordinates;

                    // Draw this road segment on screen                    
                    await DrawSegment(
                        p1.X, p1.Y, p1.W, 
                        p2.X, p2.Y, p2.W, 
                        currentRoadSegment.RoadColour,
                        currentRoadSegment.GrassColour,
                        currentRoadSegment.RumbleColour,
                        LaneColour);

                    // move the clipping bottom line up
                    clipBottomLine = currBottomLine;
                }
            }            
        }

        /// <summary>
        /// Initialises everything for the road
        /// </summary>
        /// <returns></returns>
        public async Task InitialiseAsync()
        {
            // Create initial segments
            CreateRoad();

            // Set some variables so we don't have to keep re-calculating them later
            TotalRoadSegmentCount = RoadSegments.Count;
            RoadLength = TotalRoadSegmentCount * IndividualRoadSegmentLength;

            // Colour start/end of the road
            for (var i = 0; i < RumbleSegments; i++)
            {
                // Start...
                var startSegment = RoadSegments[i];
                startSegment.RoadColour = Color.FromArgb(255, 255, 255);
                RoadSegments[i] = startSegment;

                // Finish...
                var finishSegment = RoadSegments[TotalRoadSegmentCount - 1 - i];
                finishSegment.RoadColour = Color.FromArgb(50, 50, 50);
                RoadSegments[TotalRoadSegmentCount - 1 - i] = finishSegment;
            }

            Camera.Initialise();

            await Task.CompletedTask;
        }

        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            Camera.Update(0, RoadPosition, RoadWidth, RoadLength);
            
            var kstate = KeyboardService.GetState();

            if (kstate.IsKeyDown(Keys.UpArrow))
            {
                RoadPosition += 100;

                if (RoadPosition > RoadLength) RoadPosition = IndividualRoadSegmentLength;
            }
        }

        #endregion

        #region Protected Methods
        
        protected void CreateRoad()
        {            
            CreateSection(300);
        }

        protected void CreateSection(int numberOfSegments)
        {            
            for (var i = 0; i < numberOfSegments; i++)
            {
                CreateRoadSegment();
            }
        }

        protected void CreateRoadSegment()
        {
            var n = RoadSegments.Count;

            RoadSegments.Add(new RoadSegment
            {
                Index = n,
                Point = new Point
                {
                    Scale = -1,
                    ScreenCoordinates = new ScreenCoordinate
                    {
                        X = 0,
                        Y = 0,
                        W = 0
                    },
                    WorldCoordinates = new WorldCoordinate
                    {
                        X = 0,
                        Y = 0,
                        Z = n * IndividualRoadSegmentLength
                    }
                },

                GrassColour = Math.Floor(n / (float)RumbleSegments) % 2 == 1 ? Color.FromArgb(16, 200, 16) : Color.FromArgb(0, 154, 0),
                RoadColour = Math.Floor(n / (float)RumbleSegments) % 2 == 1 ? Color.FromArgb(127, 127, 127) : Color.FromArgb(105, 105, 105),
                RumbleColour = Math.Floor(n / (float)RumbleSegments) % 2 == 1 ? Color.FromArgb(255, 255, 255) : Color.FromArgb(250, 0, 0)
            });            
        }

        protected async ValueTask DrawSegment(int x1, int y1, int w1, int x2, int y2, int w2, Color roadColour, Color grassColour, Color rumbleColour, Color laneColour)
        {            
            // Draw grass
            await GraphicsService.DrawFilledRectangleAsync(ColorTranslator.ToHtml(grassColour), 0, y2, GraphicsService.PlayFieldWidth, y1 - y2);

            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(roadColour), x1 - w1, y1, x1 + w1, y1, x2 + w2, y2, x2 - w2, y2);

            var rumble_w1 = w1 / 5;
            var rumble_w2 = w2 / 5;
            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(rumbleColour), x1 - w1 - rumble_w1, y1, x1 - w1, y1, x2 - w2, y2, x2 - w2 - rumble_w2, y2);
            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(rumbleColour), x1 + w1 + rumble_w1, y1, x1 + w1, y1, x2 + w2, y2, x2 + w2 + rumble_w2, y2);

            if (roadColour == RoadColourDark)
            {
                var line_w1 = (w1 / 20) / 2;
                var line_w2 = (w2 / 20) / 2;

                var lane_w1 = (w1 * 2) / RoadLanes;
                var lane_w2 = (w2 * 2) / RoadLanes;

                var lane_x1 = x1 - w1;
                var lane_x2 = x2 - w2;

                for (var i = 1; i < RoadLanes; i++)
                {
                    lane_x1 += lane_w1;
                    lane_x2 += lane_w2;

                    await GraphicsService.DrawQuadrilateralAsync(
                        ColorTranslator.ToHtml(laneColour),
                        lane_x1 - line_w1, y1,
                        lane_x1 + line_w1, y1,
                        lane_x2 + line_w2, y2,
                        lane_x2 - line_w2, y2                        
                    );
                }
            }
        }

        protected Point Project3D(Point point, float cameraX, float cameraY, float cameraZ, float cameraDepth)
        {
            // translating world coordinates to camera coordinates
            var transX = point.WorldCoordinates.X - cameraX;
            var transY = point.WorldCoordinates.Y - cameraY;
            var transZ = point.WorldCoordinates.Z - cameraZ;

            // scaling factor based on the law of similar triangles
            point.Scale = (int)(cameraDepth / transZ);

            // projecting camera coordinates onto a normalized projection plane
            var projectedX = point.Scale * transX;
            var projectedY = point.Scale * transY;
            var projectedW = point.Scale * RoadWidth;

            // scaling projected coordinates to the screen coordinates
            point.ScreenCoordinates.X = (int)Math.Round((1 + projectedX) * (GraphicsService.PlayFieldWidth / 2));
            point.ScreenCoordinates.Y = (int)Math.Round((1 - projectedY) * (GraphicsService.PlayFieldHeight / 2));
            point.ScreenCoordinates.W = (int)Math.Round((float)projectedW * (GraphicsService.PlayFieldWidth / 2));

            return point;
        }
        /*
        protected async ValueTask xDrawRoadSegment(int n, RoadSegment prevSegment, RoadSegment currSegment, int numberOfLanes = 3)
        {            
            var p1 = prevSegment.Point.ScreenCoordinates;
            var p2 = currSegment.Point.ScreenCoordinates;

            // Draw grass
            await GraphicsService.DrawFilledRectangleAsync(ColorTranslator.ToHtml(currSegment.GrassColour), 0, p2.Y, GraphicsService.PlayFieldWidth, p1.Y - p2.Y);
            
            // Draw the road
            var x1 = p1.X - p1.W;
            var y1 = p1.Y;
            var x2 = p1.X + p1.W;
            var y2 = p1.Y;
            var x3 = p2.X + p2.W;
            var y3 = p2.Y;
            var x4 = p2.X - p2.W;
            var y4 = p2.Y;

            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(currSegment.RoadColour), x1, y1, x2, y2, x3, y3, x4, y4);

            var rx1 = x1 - (p1.W / 5);
            var ry1 = y1;
            var rx2 = x1;
            var ry2 = y2;
            var rx3 = x4;
            var ry3 = y3;
            var rx4 = x4 - (p2.W / 5);
            var ry4 = y4;

            // Draw rumble strips
            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(currSegment.RumbleColour), rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4);

            rx1 = x2;
            ry1 = y1;
            rx2 = x2 + (p1.W / 5);
            ry2 = y2;
            rx3 = x3 + (p2.W / 5);
            ry3 = y3;
            rx4 = x3;
            ry4 = y4;

            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(currSegment.RumbleColour), rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4);

            //if (true)//rumble == Color.FromArgb(255, 255, 255))
            //{
            //    var line_w1 = (p1.W / 20) / 2;
            //    var line_w2 = (p2.W / 20) / 2;

            //    var lane_w1 = (p1.W * 2) / numberOfLanes;
            //    var lane_w2 = (p2.W * 2) / numberOfLanes;

            //    var lane_x1 = x1 - p1.W;
            //    var lane_x2 = x2 - p2.W;

            //    for (var i = 1; i < numberOfLanes; i++)
            //    {
            //        lane_x1 += lane_w1;
            //        lane_x2 += lane_w2;

            //        await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(rumble),
            //            lane_x1 - line_w1, y1,
            //            lane_x1 + line_w1, y1,
            //            lane_x2 + line_w2, y2,
            //            lane_x2 - line_w2, y2
            //        );
            //    }
            //}
        }
        */

        protected RoadSegment GetRoadSegment(float z)
        {
            if (z < 0) z += RoadLength;
            var index = (int)Math.Floor(z / IndividualRoadSegmentLength) % TotalRoadSegmentCount;

            return RoadSegments[index];
        }

        /// <summary>
        /// From the specified road segments index, project each segments world coordinates into 
        /// screen coordinates depending on the current camera coordinates etc...
        /// </summary>
        /// <param name="baseIndex"></param>
        protected void ProjectWorldToScreen(int baseIndex, int numberOfSegmentsToProject, int offsetZ)
        {
            //var bag = new ConcurrentBag<object>();
            //var tasks = RoadSegments.Where(x => x.(async item =>
            //{
            //    // some pre stuff
            //    var response = await GetData(item);
            //    bag.Add(response);
            //    // some post stuff
            //});
            //await Task.WhenAll(tasks);
            //var count = bag.Count;

            Parallel.For(0, numberOfSegmentsToProject, i =>
            {
                // Get the current road segment
                var currentRoadSegmentIndex = (baseIndex + i) % TotalRoadSegmentCount;
                var currentRoadSegment = RoadSegments[currentRoadSegmentIndex];

                // ...and project the world coordinates into screen coordinates
                currentRoadSegment.Point.Project3D(GraphicsService.PlayFieldWidth, GraphicsService.PlayFieldHeight, RoadWidth, Camera.X, Camera.Y, Camera.Z - offsetZ, Camera.DistanceToProjectionPlane);
                RoadSegments[currentRoadSegmentIndex] = currentRoadSegment;
            });

            // Draw the specified number of visible road segments on screen
            //for (var roadSegment = 0; roadSegment < numberOfSegmentsToProject; roadSegment++)
            //{
            //    // Get the current road segment
            //    var currentRoadSegmentIndex = (baseIndex + roadSegment) % RoadSegmentCount;
            //    var currentRoadSegment = RoadSegments[currentRoadSegmentIndex];

            //    // ...and project the world coordinates into screen coordinates
            //    currentRoadSegment.Point.Project3D(GraphicsService.PlayFieldWidth, GraphicsService.PlayFieldHeight, RoadWidth, Camera.X, Camera.Y, Camera.Z, Camera.DistanceToProjectionPlane);
            //    RoadSegments[currentRoadSegmentIndex] = currentRoadSegment;                
            //}
        }

        #endregion
    }
}
