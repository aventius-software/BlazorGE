using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGE.Input
{
    public interface IMouseService
    {
        public MouseState GetState();
    }
}
