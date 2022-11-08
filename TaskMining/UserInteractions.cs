using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public enum UserInteractions
    {
        // window
        WINDOW_FOCUS,
        WINDOW_UNFOCUS,
        WINDOW_OPEN,
        WINDOW_CLOSE,
            
        // mouse
        MOUSE_LEFT_CLICK,
        MOUSE_RIGHT_CLICK,
        MOUSE_HOLD,

        // KEYBOARD
        KEYBOARD_CLICK,
        KEYBOARD_HOLD,
        KEYBOARD_SEND_KEYS,

        // OTHER
        MANATEE // not a user interactions, but a manatee log
    }
}
