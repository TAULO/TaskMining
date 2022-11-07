using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class AnalyseCompleteTask
    {
        // repeatable complete task
        //
        public static List<CompleteTask> CompleteTasks = new List<CompleteTask>();

        public static List<CompleteTask> RepeatableCompleteTasks()
        {

            //for (int i = 0; i < CompleteTasks.Count; i++)
            //{
            //    //Console.WriteLine(CompleteTasks[i].CompleteTaskName + ":");
            //    var test = CompleteTasks.GroupBy(task => task.IndividualTasks)
            //        .Select(task => new { task.Key[i].Data.Data, task.Key[i].Data.UserInteractions })
            //        .Where()
            //        .ToList();

            //    for (int j = 0; j < test.Count; j++)
            //    {
            //        Console.WriteLine(test[j]);
            //    }
            //}

            //for (int i = 0; i < CompleteTasks.Count; i++)
            //{
            //    for (int j = 0; j < CompleteTasks[i].IndividualTasks.Count; j++)
            //    {
            //        if (CompleteTasks[4].IndividualTasks[j].Data.IsEqual(CompleteTasks[4].IndividualTasks[j].Data))
            //        {
            //            Console.WriteLine(CompleteTasks[4].IndividualTasks[j].Data.Data + "......." + CompleteTasks[4].IndividualTasks[j].Data.Data);
            //        }
            //    }
            //}
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
                total += taskCount != null ? taskCount.Counter : 0; // make exception
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
                throw new NotSupportedException("wrong type exception msg"); // make exception

            int total = 0;
            foreach (var task in CompleteTasks)
            {
                var taskCount = task.IndividualTasks
                    .GroupBy(task => task.Data.UserInteractions)
                    .Select(ui => new { Element = ui.Key, Counter = ui.Count() })
                    .Where(task => task.Element.ToString().Equals(userInteraction) || task.Element.Equals(userInteraction))
                    .FirstOrDefault();
                total += taskCount != null ? taskCount.Counter : 0; // make exception
            }
            return total;
        }

        public static Dictionary<string, int> IndividualTaskTotalFrequency()
        {
            var result = new Dictionary<string, int>();

            foreach (var task in CompleteTasks)
            {
                var taskDic = task.IndividualTasks
                    .GroupBy(task => task.Data.Data)
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

        // dic in dic??
        public static Dictionary<string, Dictionary<string, int>> TestTest()
        {
            var outDic = new Dictionary<string, Dictionary<string, int>>();

            foreach (var task in CompleteTasks)
            {
                Console.WriteLine(task.CompleteTaskName + ":");
                for (int i = 0; i < task.IndividualTasks.Count; i++)
                {
                    var innerDic = task.IndividualTasks
                        .GroupBy(task => new { Data = task.Data.Data, UI = task.Data.UserInteractions.ToString() })
                        .Where(t => t.Key.Data.Equals(task.IndividualTasks[i].Data.Data) && t.Key.UI.Equals(task.IndividualTasks[i].Data.UserInteractions.ToString()))
                        .Select(data => new { Element = data.Key, Counter = data.Count() })
                        .ToDictionary(dic => new { dic.Element, dic.Counter });

                    foreach (var dic in innerDic)
                    {
                        outDic.Add(task.CompleteTaskName, new Dictionary<string, int>(dic.Key.Element.Data, dic.Value.Counter));
                    }
                }
                Console.WriteLine();
            }
            return outDic;
        }
    }
}   
