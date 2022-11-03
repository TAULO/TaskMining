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
            var task2 = new CompleteTask("CompleteTaskOne", path2);
            var task3 = new CompleteTask("CompleteTaskOne", path3);
            var task4 = new CompleteTask("CompleteTaskOne", path4);

            Console.WriteLine(task1.IndividualUserInteractionsFrequency("WINDOW_OPEN"));
        }
    }
}
