using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGE.Input
{
    public struct MouseState
    {
        public double X;
        public double Y;

        public MouseState(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
