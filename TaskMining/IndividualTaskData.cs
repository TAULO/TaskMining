using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public class IndividualTaskData : IIndividualTaskData
    {
        public string Data { get; }
        public UserInteractions UserInteractions { get; }
        public IndividualTaskData(string data, UserInteractions userInteractions) 
        { 
            Data = data;  
            UserInteractions = userInteractions;
        }

        public static UserInteractions GetUserInteractions(string userInteraction)
        {
            switch(userInteraction)
            {
                case "WINDOW_FOCUS":
                    return UserInteractions.WINDOW_FOCUS;
                case "WINDOW_UNFOCUS":
                    return UserInteractions.WINDOW_UNFOCUS;
                case "WINDOW_OPEN":
                    return UserInteractions.WINDOW_OPEN;
                case "WINDOW_CLOSE":
                    return UserInteractions.WINDOW_CLOSE;
                case "MOUSE_LEFT_CLICK":
                    return UserInteractions.MOUSE_LEFT_CLICK;
                case "MOUSE_RIGHT_CLICK":
                    return UserInteractions.MOUSE_RIGHT_CLICK;
                case "MOUSE_HOLD":
                    return UserInteractions.MOUSE_HOLD;
                case "KEYBOARD_CLICK":
                    return UserInteractions.KEYBOARD_CLICK;
                case "KEYBOARD_HOLD":
                    return UserInteractions.KEYBOARD_HOLD;
                case "KEYBOARD_SEND_KEYS":
                    return UserInteractions.KEYBOARD_SEND_KEYS;
                default:
                    return UserInteractions.MANATEE;
            }
        }

        public bool IsEqual(IndividualTaskData data)
        {
            return Data.Equals(data.Data) && UserInteractions.Equals(data.UserInteractions);
        }

        override
        public string ToString() { return Data + " " + UserInteractions; }
    }   
}
