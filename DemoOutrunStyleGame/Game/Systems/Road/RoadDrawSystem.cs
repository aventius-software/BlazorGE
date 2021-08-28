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
    /// Code taken/translated from the following examples, so credit where credit is due. Well
    /// worth checking out for improving and extending this basic version:-
    /// https://www.youtube.com/watch?v=N60lBZDEwJ8&list=PLB_ibvUSN7mzUffhiay5g5GUHyJRO4DYr&index=8
    /// http://www.extentofthejam.com/pseudo/
    /// https://github.com/ssusnic/Pseudo-3d-Racer    
    /// https://codeincomplete.com/articles/javascript-racer-v1-straight/
    /// </summary>
    public class RoadDrawSystem : IGameDrawSystem, IGameUpdateSystem, IGameInitialisationSystem
    {
        #region Services
        
        protected IGraphicsService2D GraphicsService;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Road Colours

        protected Color GrassColourDark = Color.FromArgb(0, 154, 0); 
        protected Color GrassColourLight = Color.FromArgb(16, 200, 16);
        protected Color RoadColourDark = Color.FromArgb(105, 105, 105);
        protected Color RoadColourLight = Color.FromArgb(127, 127, 127);
        protected Color RumbleColourDark = Color.FromArgb(250, 0, 0);
        protected Color RumbleColourLight = Color.FromArgb(255, 255, 255);
        protected Color LaneColour = Color.FromArgb(255, 255, 255);

        #endregion

        #region Protected Properties

        protected Camera Camera = new(); // Camera to move along the track
        protected int DrawDistance = 200; // Number of segments to draw on screen at once                
        protected int IndividualSegmentLength = 100; // Length of an individual segment (from top to bottom)
        protected Player Player = new();
        protected int RoadLanes = 3; // Number of lanes for our track
        protected List<RoadSegment> RoadSegments = new(); // List of all the road segments (maybe should make an array?)
        protected int RoadWidth = 1000; // Half the width of the road (e.g. 1920px / 2 = 960)
        protected int RumbleSegments = 5; // Number of road segments that make a rumble strip
        protected int TotalRoadSegments; // The total number of road segments in the track
        protected int TrackLength; // The total track length (i.e. segment length * number of segments)

        #endregion

        #region Constructors

        public RoadDrawSystem(IGraphicsService2D graphicsService, IKeyboardService keyboardService)
        {
            GraphicsService = graphicsService;
            KeyboardService = keyboardService;
        }

        #endregion

        #region Implementations

        public async ValueTask DrawAsync(GameTime gameTime)
        {
            var clipBottomLine = GraphicsService.CanvasHeight;

            var baseSegment = GetRoadSegment(Camera.Z);
            var baseIndex = baseSegment.Index;

            for (var n = 0; n < DrawDistance; n++)
            {
                var currIndex = (baseIndex + n) % TotalRoadSegments;
                var currSegment = RoadSegments[currIndex];

                var offsetZ = (currIndex < baseIndex) ? TrackLength : 0;
                Project3D(ref currSegment.ZMap, Camera.X, Camera.Y, Camera.Z - offsetZ, Camera.DistanceToProjectionPlane);
                RoadSegments[currIndex] = currSegment;

                var currBottomLine = currSegment.ZMap.ScreenCoordinates.Y;

                if (n > 0 && currBottomLine < clipBottomLine)
                {
                    var prevIndex = currIndex > 0 ? currIndex - 1 : TotalRoadSegments - 1;
                    var prevSegment = RoadSegments[prevIndex];

                    var p1 = prevSegment.ZMap.ScreenCoordinates;
                    var p2 = currSegment.ZMap.ScreenCoordinates;

                    await DrawSegmentAsync(
                        p1.X, p1.Y, p1.W,
                        p2.X, p2.Y, p2.W,
                        currSegment.RoadColour,
                        currSegment.GrassColour,
                        currSegment.RumbleColour,
                        LaneColour
                    );

                    clipBottomLine = currBottomLine;
                }
            }
        }
        
        public async Task InitialiseAsync()
        {            
            Camera.Init();
            Player.Init();

            // Create road
            Create();                       

            Player.Restart();

            await Task.CompletedTask;
        }

        public async ValueTask UpdateAsync(GameTime gameTime)
        {
            var dt = Math.Min(1, gameTime.TimeSinceLastFrame / 1000);

            Player.Update(dt, TrackLength);
            Camera.Update(Player.X, Player.Z, RoadWidth, TrackLength);
            
            await Task.CompletedTask;
        }

        #endregion

        #region Protected Methods

        protected void Create()
        {            
            CreateRoad();

            TotalRoadSegments = RoadSegments.Count;
            TrackLength = IndividualSegmentLength * TotalRoadSegments;

            // Colour start/end of the road
            for (var i = 0; i < RumbleSegments; i++)
            {
                // Start...
                var startSegment = RoadSegments[i];
                startSegment.RoadColour = Color.FromArgb(255, 255, 255);
                RoadSegments[i] = startSegment;

                // Finish...
                var finishSegment = RoadSegments[TotalRoadSegments - 1 - i];
                finishSegment.RoadColour = Color.FromArgb(50, 50, 50);
                RoadSegments[TotalRoadSegments - 1 - i] = finishSegment;
            }
        }
        
        protected void CreateRoad()
        {
            CreateSection(300);
        }

        protected void CreateSection(int numberOfSegments)
        {
            for (var i=0;i<numberOfSegments;i++)
            {
                CreateSegment();
            }
        }

        protected void CreateSegment()
        {
            var n = RoadSegments.Count;

            RoadSegments.Add(new RoadSegment
            {
                Index = n,
                ZMap = new ZMap
                {                   
                    WorldCoordinates = new WorldCoordinate
                    {
                        X = 0,
                        Y = 0,
                        Z = n * IndividualSegmentLength
                    },
                    ScreenCoordinates = new ScreenCoordinate
                    {
                        X = 0,
                        Y = 0,
                        W = 0
                    },
                    Scale = -1
                },                
                GrassColour = Math.Floor(n / (float)RumbleSegments) % 2 == 1 ? GrassColourLight : GrassColourDark,
                RoadColour = Math.Floor(n / (float)RumbleSegments) % 2 == 1 ? RoadColourLight : RoadColourDark,
                RumbleColour = Math.Floor(n / (float)RumbleSegments) % 2 == 1 ? RumbleColourLight : RumbleColourDark
            });            
        }

        protected void Project2D(ref ZMap zmap)
        {
            zmap.ScreenCoordinates.X = GraphicsService.CanvasWidth / 2;
            zmap.ScreenCoordinates.Y = (int)(GraphicsService.CanvasHeight - zmap.WorldCoordinates.Z);
            zmap.ScreenCoordinates.W = RoadWidth;
        }

        protected void Project3D(ref ZMap zmap, float cameraX, float cameraY, float cameraZ, float cameraDepth)
        {
            // translating world coordinates to camera coordinates
            var transX = zmap.WorldCoordinates.X - cameraX;
            var transY = zmap.WorldCoordinates.Y - cameraY;
            var transZ = zmap.WorldCoordinates.Z - cameraZ;

            // scaling factor based on the law of similar triangles
            zmap.Scale = cameraDepth / transZ;

            // projecting camera coordinates onto a normalized projection plane
            var projectedX = zmap.Scale * transX;
            var projectedY = zmap.Scale * transY;
            var projectedW = zmap.Scale * RoadWidth;

            // scaling projected coordinates to the screen coordinates
            zmap.ScreenCoordinates.X = (int)Math.Round((1 + projectedX) * (GraphicsService.CanvasWidth / 2));
            zmap.ScreenCoordinates.Y = (int)Math.Round((1 - projectedY) * (GraphicsService.CanvasHeight / 2));
            zmap.ScreenCoordinates.W = (int)Math.Round(projectedW * (GraphicsService.CanvasWidth / 2));
        }
            
        protected async ValueTask DrawSegmentAsync(int x1, int y1, int w1, int x2, int y2, int w2, Color roadColour, Color grassColour, Color rumbleColour, Color laneColour)
        {
            // Draw grass first
            await GraphicsService.DrawFilledRectangleAsync(ColorTranslator.ToHtml(grassColour), 0, y2, GraphicsService.CanvasWidth, y1 - y2);

            // Draw the road surface
            await GraphicsService.DrawQuadrilateralAsync(ColorTranslator.ToHtml(roadColour), x1 - w1, y1, x1 + w1, y1, x2 + w2, y2, x2 - w2, y2);

            // Draw rumble strips
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

        protected RoadSegment GetRoadSegment(float z)
        {
            if (z < 0) z += TrackLength;
            var index = (int)Math.Floor(z / IndividualSegmentLength) % TotalRoadSegments;

            return RoadSegments[index];
        }

        #endregion
    }
}
