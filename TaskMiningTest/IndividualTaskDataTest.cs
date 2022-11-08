using TaskMining;

namespace TaskMiningTest
{
    [TestClass]
    public class IndividualTaskDataTest
    {
        [TestMethod]
        public void TestUserInteractions()
        {
            IndividualTaskData winFoc = new IndividualTaskData("some data", UserInteractions.WINDOW_FOCUS);
            IndividualTaskData winUnfoc = new IndividualTaskData("some data", UserInteractions.WINDOW_UNFOCUS);
            IndividualTaskData winOpen = new IndividualTaskData("some data", UserInteractions.WINDOW_OPEN);
            IndividualTaskData winClose = new IndividualTaskData("some data", UserInteractions.WINDOW_CLOSE);
            IndividualTaskData mouseLef = new IndividualTaskData("some data", UserInteractions.MOUSE_LEFT_CLICK);
            IndividualTaskData mouseRigth = new IndividualTaskData("some data", UserInteractions.MOUSE_RIGHT_CLICK);
            IndividualTaskData mouseHold = new IndividualTaskData("some data", UserInteractions.MOUSE_HOLD);
            IndividualTaskData keyClick = new IndividualTaskData("some data", UserInteractions.KEYBOARD_CLICK);
            IndividualTaskData keyHold = new IndividualTaskData("some data", UserInteractions.KEYBOARD_HOLD);
            IndividualTaskData KeySend = new IndividualTaskData("some data", UserInteractions.KEYBOARD_SEND_KEYS);
            IndividualTaskData manatee = new IndividualTaskData("some data", UserInteractions.MANATEE);

            Assert.AreEqual(UserInteractions.WINDOW_FOCUS, winFoc.UserInteractions);
            Assert.AreEqual(UserInteractions.WINDOW_UNFOCUS, winUnfoc.UserInteractions);
            Assert.AreEqual(UserInteractions.WINDOW_OPEN, winOpen.UserInteractions);
            Assert.AreEqual(UserInteractions.WINDOW_CLOSE, winClose.UserInteractions);
            Assert.AreEqual(UserInteractions.MOUSE_LEFT_CLICK, mouseLef.UserInteractions);
            Assert.AreEqual(UserInteractions.MOUSE_RIGHT_CLICK, mouseRigth.UserInteractions);
            Assert.AreEqual(UserInteractions.MOUSE_HOLD, mouseHold.UserInteractions);
            Assert.AreEqual(UserInteractions.KEYBOARD_CLICK, keyClick.UserInteractions);
            Assert.AreEqual(UserInteractions.KEYBOARD_HOLD, keyHold.UserInteractions);
            Assert.AreEqual(UserInteractions.KEYBOARD_SEND_KEYS, KeySend.UserInteractions);
            Assert.AreEqual(UserInteractions.MANATEE, manatee.UserInteractions);
        }

        [TestMethod]
        public void TestGetUserInteractions()
        {
            Assert.AreEqual(UserInteractions.WINDOW_FOCUS, IndividualTaskData.GetUserInteractions("WINDOW_FOCUS"));
            Assert.AreEqual(UserInteractions.WINDOW_UNFOCUS, IndividualTaskData.GetUserInteractions("WINDOW_UNFOCUS"));
            Assert.AreEqual(UserInteractions.WINDOW_OPEN, IndividualTaskData.GetUserInteractions("WINDOW_OPEN"));
            Assert.AreEqual(UserInteractions.WINDOW_CLOSE, IndividualTaskData.GetUserInteractions("WINDOW_CLOSE"));
            Assert.AreEqual(UserInteractions.MOUSE_LEFT_CLICK, IndividualTaskData.GetUserInteractions("MOUSE_LEFT_CLICK"));
            Assert.AreEqual(UserInteractions.MOUSE_RIGHT_CLICK, IndividualTaskData.GetUserInteractions("MOUSE_RIGHT_CLICK"));
            Assert.AreEqual(UserInteractions.MOUSE_HOLD, IndividualTaskData.GetUserInteractions("MOUSE_HOLD"));
            Assert.AreEqual(UserInteractions.KEYBOARD_CLICK, IndividualTaskData.GetUserInteractions("KEYBOARD_CLICK"));
            Assert.AreEqual(UserInteractions.KEYBOARD_HOLD, IndividualTaskData.GetUserInteractions("KEYBOARD_HOLD"));
            Assert.AreEqual(UserInteractions.KEYBOARD_SEND_KEYS, IndividualTaskData.GetUserInteractions("KEYBOARD_SEND_KEYS"));
            Assert.AreEqual(UserInteractions.MANATEE, IndividualTaskData.GetUserInteractions("MANATEE"));
        }
    }
}
