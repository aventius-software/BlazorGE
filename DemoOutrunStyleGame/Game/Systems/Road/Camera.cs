namespace DemoOutrunStyleGame.Game.Systems.Road
{
    public struct Camera
    {
        public float DistanceToPlayer;
        public float DistanceToProjectionPlane;
        public float X;
        public float Y;
        public float Z;                

        public void Initialise()
        {
            X = 0;
            Y = 1000;
            Z = 0;
            DistanceToPlayer = 500;
            DistanceToProjectionPlane = 1 / (Y / DistanceToPlayer);
        }

        public void Update(float playerX, float playerZ, int roadWidth, int roadLength)
        {
            // since player X is normalized within [-1, 1], then camera X must be multiplied by road width
            X = playerX * roadWidth;

            // place the camera behind the player at the desired distance
            Z = playerZ - DistanceToPlayer;

            // don't let camera Z to go negative
            if (Z < 0) Z += roadLength;
        }
    }
}
