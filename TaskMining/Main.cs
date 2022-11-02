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
            string path = "C:/Users/Taulo/Desktop/TaskMiningTestData/CalcWorkFlow/CalcWorkFlow1.txt";
            var taskOneData = new HandleCSVData(path);


            taskOneData.Tasks.ForEach(task =>
            {
                //Console.WriteLine(task.TotalTaskCompletionTimeInSeconds(taskOneData.Tasks[0], taskOneData.Tasks[taskOneData.Tasks.Count - 1]));
                var time = task.TotalTaskCompletionTime(taskOneData.Tasks[0], taskOneData.Tasks[taskOneData.Tasks.Count - 1]);
                Console.WriteLine("Total time: " + time.Minute + ":" + time.Second);
                //Console.WriteLine(task.GetDateTime());
            });       
        }
    }
}
