#region Namespace

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace DemoOutrunStyleGame.Game.Systems.Road
{
    public struct RoadSegment
    {
        public int Index;
        public int Scale;
        public ZMap ZMap;

        #region Colours

        public Color GrassColour;
        public Color LaneColour;
        public Color RoadColour;
        public Color RumbleColour;

        #endregion
    }
}
