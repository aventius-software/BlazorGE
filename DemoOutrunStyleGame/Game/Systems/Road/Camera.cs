namespace DemoOutrunStyleGame.Game.Systems.Road
{
    public class Camera
    {
        public float DistanceToPlayer = 500;
        public float DistanceToProjectionPlane = 0;
        public float X = 0;
        public float Y = 1000;
        public float Z = 0;
        
        public Camera()
        {
            X = 0;
            Y = 1000;
            Z = 0;

            // Z-distance between camera and player
            DistanceToPlayer = 500;

            // Z-distance between camera and normalized projection plane
            DistanceToProjectionPlane = 0;
        }

        public void Init()
        {
            DistanceToProjectionPlane = 1 / (Y / DistanceToPlayer);
        }

        public void Update(float playerX, float playerZ, int roadWidth, int trackLength)
        {
            X = playerX * roadWidth;
            Z = playerZ - DistanceToPlayer;

            // don't let camera Z to go negative
            if (Z < 0) Z += trackLength;
        }
    }
}
