using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGE.Input
{
    public class MouseService : IMouseService
    {
        protected MouseState MouseState = new MouseState(0, 0);

        public MouseState GetState()
        {
            return MouseState;
        }
    }
}
