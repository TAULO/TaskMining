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
            string pathCopi = "C:/Users/Taulo/Desktop/Task Mining source code/TaskMining/TaskMining/TaskMiningUserData/CalcWorkFlow/CalcWorkFlow1 - Kopi.txt";

            var task1 = new CompleteTask("CompleteTaskOne", path1);
            var task2 = new CompleteTask("CompleteTaskTwo", path2);
            var task3 = new CompleteTask("CompleteTaskThree", path3);
            var task4 = new CompleteTask("CompleteTaskFour", path4);
            var taskKopi = new CompleteTask("CompleteTaskKopi", pathCopi);

            var tasks = AnalyseCompleteTask.CompleteTasks = new List<CompleteTask>() { task1, task2, task3, task4, taskKopi };

            //Console.WriteLine(task1.TimeSpentPrApplication()[1]);

            foreach(var task in task1.TimeSpentPrApplication())
            {
                Console.WriteLine(task);
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
        }
    }
}
