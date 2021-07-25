using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoOutrunStyleGame.Game.Systems.Road
{
    public class Player
    {
		public float X, Y, Z;
		public float Speed;
		public float MaxSpeed;

		public Player()
        {
			X = 0;
			Y = 0;
			Z = 0;

			MaxSpeed = 100f / (1f / 60f);

			Speed = 0;
		}
		
		public void Init()
        {

        }

		public void Restart()
        {
			X = 0;
			Y = 0;
			Z = 0;

			Speed = MaxSpeed;
		}

		public void Update(float dt, int roadLength)
		{
			// ---------------------------------------------------------------------------------
			// Moving in Z-direction
			// ---------------------------------------------------------------------------------

			Z += Speed * dt;
			if (Z >= roadLength) Z -= roadLength;
		}
	}
}
