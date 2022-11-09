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
            Assert.AreEqual(task.TotalTasksCompletionTimeInSeconds, 57);
        }

        [TestMethod]
        public void TestIndividualTaskFrequency()
        {
            CompleteTask task = GetData();

            int calc = task.IndividualTaskFrequency("Calculator");
            int docker = task.IndividualTaskFrequency("Docker");
            int excel = task.IndividualTaskFrequency("Excel");
            int five = task.IndividualTaskFrequency("5");
            int plus = task.IndividualTaskFrequency("+");
            int ten = task.IndividualTaskFrequency("10");
            int divide = task.IndividualTaskFrequency("/");
            int two = task.IndividualTaskFrequency("2");
            int equal = task.IndividualTaskFrequency("=");
            int helloWorld = task.IndividualTaskFrequency("Hello World");
            int start = task.IndividualTaskFrequency("start");

            Assert.AreEqual(6, calc, "calculator");
            Assert.AreEqual(3, docker, "docker");
            Assert.AreEqual(7, excel, "excel");
            Assert.AreEqual(1, five, "5");
            Assert.AreEqual(1, plus, "+");
            Assert.AreEqual(2, ten, "10");
            Assert.AreEqual(1, divide, "/");
            Assert.AreEqual(1, two, "2");
            Assert.AreEqual(1, equal, "=");
            Assert.AreEqual(1, helloWorld, "hello world");
            Assert.AreEqual(1, start, "start");

            // test case sensitive
            int calcCase = task.IndividualTaskFrequency("CalCulaToR");
            int dockerCase = task.IndividualTaskFrequency("docker");
            int excelCase = task.IndividualTaskFrequency("EXCEL");

            Assert.AreEqual(6, calcCase, "calculator case sensitive");
            Assert.AreEqual(3, dockerCase, "docker case sensitive");
            Assert.AreEqual(7, excelCase, "excel case sensitive");
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
            Assert.AreEqual(3, winOpen);
            Assert.AreEqual(6, winFocus);
            Assert.AreEqual(2, keyClick);
            Assert.AreEqual(1 ,keySend);
            Assert.AreEqual(6, winUnfocus);
            Assert.AreEqual(1, winClosed);
        }

        [TestMethod]
        public void TestTimespentPrApplication()
        {
            CompleteTask task = GetData();
            Dictionary<string, double> dic = task.TimeSpentPrApplication;

            double calc = dic["Calculator"];
            double excel = dic["Excel"];
            double docker = dic["Docker"];

            Assert.AreEqual(11, calc, "calculator");
            Assert.AreEqual(18, excel, "excel");
            Assert.AreEqual(10, docker, "docker");
        }

        [TestMethod] 
        public void TestTotalCompleteTaskApplicationsUsed()
        {
            CompleteTask task = GetData();
            int totalApps = task.TotalCompleteTaskApplicationsUsed;
            
            Assert.AreEqual(3, totalApps, "total apps");
        }
        public void TestTotalAmountOfUserInteractionActions()
        {
            CompleteTask task = GetData();
            int totalUserInteractions = task.TotalAmountOfUserInteractionActions;

            Assert.AreEqual(27, totalUserInteractions, "total user interactions");
        }
    }
}
