using TaskMining;

namespace TaskMiningTest
{
    [TestClass]
    public class AnalyseCompleteTaskTest
    {
        public void LoadData()
        {
            string path1 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMiningTest/TaskMiningTestUserData/CalcWorkFlow/CalcWorkFlow1.txt";
            string path2 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMiningTest/TaskMiningTestUserData/CalcWorkFlow/CalcWorkFlow2.txt";
            string path3 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMiningTest/TaskMiningTestUserData/CalcWorkFlow/CalcWorkFlow3.txt";
            string path4 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMiningTest/TaskMiningTestUserData/CalcWorkFlow/CalcWorkFlow4.txt";

            string pathCopy = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow1 - Kopi.txt";
            string pathCopy1 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow1 - Kopi1.txt";

            var task1 = new CompleteTask("CompleteTaskOne", path1);
            var task2 = new CompleteTask("CompleteTaskTwo", path2);
            var task3 = new CompleteTask("CompleteTaskThree", path3);
            var task4 = new CompleteTask("CompleteTaskFour", path4);

            var taskKopi = new CompleteTask("CompleteTaskKopi", pathCopy);
            var taskKopi1 = new CompleteTask("CompleteTaskKopi1", pathCopy1);

            AnalyseCompleteTask.CompleteTasks = new List<CompleteTask>() { task1, task2, task3, task4, taskKopi, taskKopi1 };
        }

        [TestMethod]
        public void TestIndividualTaskTotalFrequency()
        {
            LoadData();
            
            int plus = AnalyseCompleteTask.IndividualTaskTotalFrequency("+");
            int ten = AnalyseCompleteTask.IndividualTaskTotalFrequency("10");
            int calc = AnalyseCompleteTask.IndividualTaskTotalFrequency("Calculator");
            int excel = AnalyseCompleteTask.IndividualTaskTotalFrequency("Excel");
            int docker = AnalyseCompleteTask.IndividualTaskTotalFrequency("Docker");

            Assert.AreEqual(6, plus, "+");
            Assert.AreEqual(12, ten, "10");
            Assert.AreEqual(26, calc, "calculator");
            Assert.AreEqual(6, excel, "excel");
            Assert.AreEqual(2, docker, "docker");

            // test case sensitive
            int calcCase = AnalyseCompleteTask.IndividualTaskTotalFrequency("CalCulaToR");

            Assert.AreEqual(26, calcCase, "calculator case sensitive");
        }

        [TestMethod]
        public void TestIndividualUserInteractionsTotalFrequency()
        {
            LoadData();

            int manatee = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("MANATEE");
            int winOpen = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("WINDOW_OPEN");
            int winFocus = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("WINDOW_FOCUS");
            int keyClick = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("KEYBOARD_CLICK");
            int keySend = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("KEYBOARD_SEND_KEYS");
            int winUnfocus = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("WINDOW_UNFOCUS");
            int winClosed = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency("WINDOW_CLOSE");

            Assert.AreEqual(30, manatee);
            Assert.AreEqual(6, winOpen);
            Assert.AreEqual(11, winFocus);
            Assert.AreEqual(12, keyClick);
            Assert.AreEqual(4, keySend);
            Assert.AreEqual(11, winUnfocus);
            Assert.AreEqual(6, winClosed);
        }

        [TestMethod]
        public void TestIndividualTaskTotalFrequencyDic()
        {
            LoadData();

            var dic = AnalyseCompleteTask.IndividualTaskTotalFrequency();

            int calc = dic["Calculator"];
            int ten = dic["10"];
            int plus = dic["+"];
            int five = dic["5"];
            int times = dic["*"];
            int divide = dic["/"];
            int two = dic["2"];
            int equal = dic["="];
            int excel = dic["Excel"];
            int docker = dic["Docker"];

            Assert.AreEqual(26, calc);
            Assert.AreEqual(12, ten);
            Assert.AreEqual(6, plus);
            Assert.AreEqual(6, five);
            Assert.AreEqual(6, times);
            Assert.AreEqual(6, divide);
            Assert.AreEqual(6, two);
            Assert.AreEqual(6, equal);
            Assert.AreEqual(6, excel);
            Assert.AreEqual(2, docker);
        }

        [TestMethod]
        public void TestIndividualUserInteractionsTotalFrequencyDic()
        {
            LoadData();

            var dic = AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency();

            int manatee = dic["MANATEE"];
            int winOpen = dic["WINDOW_OPEN"];
            int winFocus = dic["WINDOW_FOCUS"];
            int mouseLeftClick = dic["MOUSE_LEFT_CLICK"];
            int winUnfocus = dic["WINDOW_UNFOCUS"];
            int winClose = dic["WINDOW_CLOSE"];
            int keySendKeys = dic["KEYBOARD_SEND_KEYS"];
            int keyClick = dic["KEYBOARD_CLICK"];

            Assert.AreEqual(30, manatee);
            Assert.AreEqual(6, winOpen);
            Assert.AreEqual(11, winFocus);
            Assert.AreEqual(32, mouseLeftClick);
            Assert.AreEqual(11, winUnfocus);
            Assert.AreEqual(6, winClose);
            Assert.AreEqual(4, keySendKeys);
            Assert.AreEqual(12, keyClick);
        }
    }
}
