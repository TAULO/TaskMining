using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal enum UserInteractions
    {
        // window
        WINDOW_FOCUS,
        WINDOW_UNFOCUS,
            
        // mouse
        MOUSE_LEFT_CLICK,
        MOUSE_RIGHT_CLICK,
        MOUSE_HOLD,

        // KEYBOARD
        KEYBOARD_CLICK,
        KEYBOARD_HOLD,

        // OTHER
        OTHER
    }
}
