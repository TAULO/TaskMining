using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public class TaskMining
    {
        public static void Main(string[] args)
        {
            string path1 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow1.txt";
            string path2 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow2.txt";
            string path3 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow3.txt";
            string path4 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow4.txt";
            
            string pathCopy = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow1 - Kopi.txt";
            string pathCopy1 = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow1 - Kopi1.txt";

            var task1 = new CompleteTask("CompleteTaskOne", path1);
            var task2 = new CompleteTask("CompleteTaskTwo", path2);
            var task3 = new CompleteTask("CompleteTaskThree", path3);
            var task4 = new CompleteTask("CompleteTaskFour", path4);

            var taskKopi = new CompleteTask("CompleteTaskKopi", pathCopy);
            var taskKopi1 = new CompleteTask("CompleteTaskKopi1", pathCopy1);

            var tasks = AnalyseCompleteTask.CompleteTasks = new List<CompleteTask>() { task1, task2, task3, task4, taskKopi, taskKopi1 };

            foreach (var item in task2.TimeSpentPrApplication)
            {
                //Console.WriteLine(item);
            }

            //foreach (var completeTask in tasks)
            //{
            //    Console.WriteLine(completeTask.CompleteTaskName);
            //    Console.WriteLine();
            //    foreach (var individualTasks in completeTask.IndividualTasks)
            //    {
            //        Console.WriteLine(individualTasks.Data.Data + "........................" + individualTasks.Data.UserInteractions);
            //    }
            //    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            //    Console.WriteLine();
            //}

            AnalyseCompleteTask.RepeatableCompleteTasks();
            //Console.WriteLine(AnalyseCompleteTask.IndividualTaskTotalFrequency("10"));
            //Console.WriteLine(AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency(UserInteractions.MOUSE_LEFT_CLICK));

            //foreach (var item in AnalyseCompleteTask.IndividualTaskTotalFrequency())
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency())
            //{
            //    Console.WriteLine(item);
            //}

            AnalyseCompleteTask.TestTest();
        }
    }
}
