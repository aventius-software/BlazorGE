using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGE.Input
{
    public class MouseService : IMouseService
    {
        private MouseState MouseState = new MouseState(0, 0, KeyState.Up);

        public MouseState GetState()
        {
            return MouseState;
        }

        public void SetState(MouseState state)
        {
            MouseState = state;
        }

    }
}
