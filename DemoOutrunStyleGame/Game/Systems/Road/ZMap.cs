#region Namespaces

using System;

#endregion

namespace DemoOutrunStyleGame.Game.Systems.Road
{
    public struct ZMap
    {
        public float Scale;
        public ScreenCoordinate ScreenCoordinates;
        public WorldCoordinate WorldCoordinates;

        //public void ProjectWorldToScreen(int screenWidth, int screenHeight, int roadWidth, float cameraX, float cameraY, float cameraZ, float cameraDistanceToScreen)
        //{
        //    var transX = WorldCoordinates.X - cameraX;
        //    var transY = WorldCoordinates.Y - cameraY;
        //    var transZ = WorldCoordinates.Z - cameraZ;

        //    Scale = cameraDistanceToScreen / transZ;

        //    var projectX = Scale * transX;
        //    var projectY = Scale * transY;
        //    var projectW = Scale * roadWidth;

        //    ScreenCoordinates.X = (int)Math.Round((1 + projectX) * (screenWidth / 2));
        //    ScreenCoordinates.Y = (int)Math.Round((1 - projectY) * (screenHeight / 2));
        //    ScreenCoordinates.W = (int)Math.Round(projectW * (screenWidth / 2));
        //}
    }
}
