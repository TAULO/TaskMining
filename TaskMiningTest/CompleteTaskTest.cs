using TaskMining;

namespace TaskMiningTest
{
    [TestClass]
    public class CompleteTaskTest
    {
        public CompleteTask GetData()
        {
            string path = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlowTestData.txt";
            return new CompleteTask("CompleteTask", path);
        } 

        [TestMethod]
        public void TotalTaskCompletionTimeInSecondsTest()
        {
            CompleteTask task = GetData();
            Assert.AreEqual(task.TotalTasksCompletionTimeInSeconds, 18);
        }

        [TestMethod]
        public void TestIndividualTaskFrequency()
        {
            CompleteTask task = GetData();

            int app = task.IndividualTaskFrequency("Calculator");
            int five = task.IndividualTaskFrequency("5");
            int plus = task.IndividualTaskFrequency("+");
            int ten = task.IndividualTaskFrequency("10");
            int divide = task.IndividualTaskFrequency("/");
            int two = task.IndividualTaskFrequency("2");
            int equal = task.IndividualTaskFrequency("=");

            Assert.AreEqual(4, app);
            Assert.AreEqual(1, five);
            Assert.AreEqual(1, plus);
            Assert.AreEqual(2, ten);
            Assert.AreEqual(2, divide);
            Assert.AreEqual(1, two);
            Assert.AreEqual(1, equal);
        }

        [TestMethod]
        public void TestIndividualUserInteractionsFrequency()
        {
            CompleteTask task = GetData();

            int manatee = task.IndividualUserInteractionsFrequency("MANATEE");
            int winOpen = task.IndividualUserInteractionsFrequency("WINDOW_OPEN");
            int winFocus = task.IndividualUserInteractionsFrequency("WINDOW_FOCUS");
            int keyClick = task.IndividualUserInteractionsFrequency("KEYBOARD_CLICK");
            int keySend = task.IndividualUserInteractionsFrequency("KEYBOARD_SEND_KEYS");
            int winUnfocus = task.IndividualUserInteractionsFrequency("WINDOW_UNFOCUS");
            int winClosed = task.IndividualUserInteractionsFrequency("WINDOW_CLOSE");

            Assert.AreEqual(5, manatee);
            Assert.AreEqual(1, winOpen);
            Assert.AreEqual(1, winFocus);
            Assert.AreEqual(6, keyClick);
            Assert.AreEqual(2 ,keySend);
            Assert.AreEqual(1, winUnfocus);
            Assert.AreEqual(1, winClosed);
        }

        [TestMethod]
        public void TestTimespentPrApplication()
        {
            CompleteTask task = GetData();
            Dictionary<string, double> data = task.TimeSpentPrApplication;
        }
    }
}
