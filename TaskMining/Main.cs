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
            string path1 = "C:/Users/Taulo/Desktop/TaskMiningTestData/CalcWorkFlow/CalcWorkFlow1.txt";
            string path2 = "C:/Users/Taulo/Desktop/TaskMiningTestData/CalcWorkFlow/CalcWorkFlow2.txt";
            string path3 = "C:/Users/Taulo/Desktop/TaskMiningTestData/CalcWorkFlow/CalcWorkFlow3.txt";
            string path4 = "C:/Users/Taulo/Desktop/TaskMiningTestData/CalcWorkFlow/CalcWorkFlow3.txt";


            var task1 = new CompleteTask("CompleteTaskOne", path1);
            var task2 = new CompleteTask("CompleteTaskTwo", path2);
            var task3 = new CompleteTask("CompleteTaskThree", path3);
            var task4 = new CompleteTask("CompleteTaskFour", path4);

            List<CompleteTask> tasks = new List<CompleteTask>() { task1, task2, task3, task4 };

            foreach (var completeTask in tasks)
            {
                Console.WriteLine(completeTask.CompleteTaskName);
                Console.WriteLine();
                foreach (var individualTasks in completeTask.IndividualTasks)
                {
                    Console.WriteLine(individualTasks.Data.Data + "........................" + individualTasks.Data.UserInteractions);
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
