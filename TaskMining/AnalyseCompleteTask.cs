using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public class AnalyseCompleteTask
    {
        public static List<CompleteTask> CompleteTasks { get => GetData(); }
        public static int TotalCompleteTasks { get => CompleteTasks.Count; }
        public static List<CompleteTask> RepeatableCompleteTasks()
        {
            return new List<CompleteTask>();
        }

        /// <summary>
        /// Gets the total amount of individual tasks a single action has occurred. 
        /// </summary>
        /// <param name="taskData">The specified action.</param>
        /// <returns>The exact number of the specified actions. If specified action not found, returns 0.</returns>
        public static int IndividualTaskTotalFrequency(string taskData)
        {
            int total = 0;
            foreach (var task in CompleteTasks)
            {
                var taskCount = task.IndividualTasks
                    .GroupBy(task => task.Data.Data)
                    .Select(data => new { Element = data.Key, Counter = data.Count() })
                    .Where(task => task.Element.ToLower().Equals(taskData.ToLower()))
                    .FirstOrDefault();

                total += taskCount != null ? taskCount.Counter : 0; // throw exception?
            }
            return total;
        }

        /// <summary>
        /// Gets the total amount of individual tasks a single user interaction has occurred. 
        /// </summary>
        /// <param name="userInteraction">The specifed action</param>
        /// <returns>The exact number of the specified actions. If specified action not found, returns 0</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static int IndividualUserInteractionsTotalFrequency(object userInteraction)
        {
            if (userInteraction.GetType() != typeof(string) && userInteraction.GetType() != typeof(UserInteractions))
                throw new NotSupportedException("wrong type exception msg"); 

            int total = 0;
            foreach (var task in CompleteTasks)
            {
                var taskCount = task.IndividualTasks
                    .GroupBy(task => task.Data.UserInteractions)
                    .Select(ui => new { Element = ui.Key, Counter = ui.Count() })
                    .Where(task => task.Element.ToString().Equals(userInteraction) || task.Element.Equals(userInteraction))
                    .FirstOrDefault();
                total += taskCount != null ? taskCount.Counter : 0; 
            }
            return total;
        }

        public static Dictionary<string, int> IndividualTaskTotalFrequency()
        {
            var result = new Dictionary<string, int>();

            foreach (var task in CompleteTasks)
            {
                var taskDic = task.IndividualTasks
                    .GroupBy(task => task.Data)
                    .Select(data => new { Ui = data.Key.UserInteractions, Data = data.Key, Counter = data.Count() })
                    .Where(ui => !ui.Ui.Equals(UserInteractions.MANATEE))
                    .ToDictionary(dic => new { dic.Data, dic.Counter });

                foreach (var dic in taskDic)
                {
                    if (!result.ContainsKey(dic.Key.Data.Data))
                    {
                        result.Add(dic.Key.Data.Data, dic.Value.Counter);
                    }
                    else
                    {
                        result[dic.Key.Data.Data] += dic.Value.Counter;
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, int> IndividualUserInteractionsTotalFrequency()
        {
            var result = new Dictionary<string, int>();

            foreach (var task in CompleteTasks)
            {
                var taskDic = task.IndividualTasks
                    .GroupBy(task => task.Data.UserInteractions.ToString())
                    .Select(data => new { Element = data.Key, Counter = data.Count() })
                    .ToDictionary(dic => new { dic.Element, dic.Counter });
                    
                foreach (var dic in taskDic)
                {
                    if (!result.ContainsKey(dic.Key.Element))
                    {
                        result.Add(dic.Key.Element, dic.Value.Counter);
                    }
                    else
                    {
                        result[dic.Key.Element] += dic.Value.Counter;
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, double> CompletionTimePrCompleteTask()
        {
            var dic = new Dictionary<string, double>();

            foreach(var task in CompleteTasks)
            {
                dic.Add(task.CompleteTaskName, task.TotalTasksCompletionTimeInSeconds);
            }
            return dic; 
        }

        public static double CompleteTaskAverageCompletionTime()
        {
            double result = 0;
            foreach (var task in CompleteTasks)
            {
                result += task.TotalTasksCompletionTimeInSeconds;
            }
            return result / CompleteTasks.Count;
        }

        // does not work, fix later   
        public static Dictionary<string, Dictionary<string, int>> TestTest()
        {
            var outDic = new Dictionary<string, Dictionary<string, int>>();
            var innerDic = new Dictionary<string, int>();

            foreach (var task in CompleteTasks)
            {
                for (int i = 0; i < task.IndividualTasks.Count; i++)
                {
                    var innerDicData = task.IndividualTasks
                        .GroupBy(task => new { Data = task.Data.Data, UI = task.Data.UserInteractions.ToString() })
                        //.Where(t => t.Key.Data.Equals(task.IndividualTasks[i].Data.Data) && t.Key.UI.Equals(task.IndividualTasks[i].Data.UserInteractions.ToString()))
                        .Select(data => new { Element = data.Key, Counter = data.Count() })
                        .ToDictionary(dic => new { dic.Element, dic.Counter });

                    if (!outDic.ContainsKey(task.CompleteTaskName))
                    {
                        foreach (var innerData in innerDicData)
                        {
                            if (!innerDic.ContainsKey(innerData.Key.Element.Data))
                            {
                                Console.WriteLine(task.CompleteTaskName + ": " + innerData.Key.Element.Data + "....." + innerData.Value.Counter);  
                                innerDic.Add(innerData.Key.Element.Data, innerData.Value.Counter);
                            } else
                            {
                                innerDic[innerData.Key.Element.Data] += innerData.Value.Counter;
                            }
                        }
                    }
                }
                outDic.Add(task.CompleteTaskName, innerDic);
            }
            return outDic;
        }

        public static List<CompleteTask> GetData()
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

            return new List<CompleteTask> { task1, task2, task3, task4, taskKopi, taskKopi1 };
        }
    }
}   
