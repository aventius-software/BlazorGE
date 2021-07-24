#region Namespaces

using System;

#endregion

namespace DemoOutrunStyleGame.Game.Systems.Road
{
    public struct Point
    {
        public WorldCoordinate WorldCoordinates;
        public ScreenCoordinate ScreenCoordinates;
        public int Scale;

        public void Project2D(int screenWidth, int screenHeight, int roadWidth)
        {
            ScreenCoordinates.X = screenWidth / 2;
            ScreenCoordinates.Y = (int)(screenHeight - WorldCoordinates.Z);
            ScreenCoordinates.W = roadWidth;
        }

        public void Project3D(int screenWidth, int screenHeight, int roadWidth, float cameraX, float cameraY, float cameraZ, float cameraDistanceToScreen)
        {
            var transX = WorldCoordinates.X - cameraX;
            var transY = WorldCoordinates.Y - cameraY;
            var transZ = WorldCoordinates.Z - cameraZ;

            var scale = cameraDistanceToScreen / transZ;

            var projectX = scale * transX;
            var projectY = scale * transY;
            var projectW = scale * roadWidth;

            ScreenCoordinates.X = (int)Math.Round((1 + projectX) * (screenWidth / 2));
            ScreenCoordinates.Y = (int)Math.Round((1 - projectY) * (screenHeight / 2));
            ScreenCoordinates.W = (int)Math.Round(projectW * (screenWidth / 2));
        }
    }
}
