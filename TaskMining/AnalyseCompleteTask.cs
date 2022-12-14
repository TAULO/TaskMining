using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public static class AnalyseCompleteTask
    {

        //public static List<CompleteTask> CompleteTasks { get => new List<CompleteTask>(); }
        public static List<CompleteTask> CompleteTasks = new List<CompleteTask>();
        public static int TotalCompleteTasks { get => CompleteTasks.Count; }
        public static List<CompleteTask> RepeatableCompleteTasks()
        {
            // group by & distinct??
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
            // find all tasks completion time and add to result
            foreach (var task in CompleteTasks)
            {
                result += task.TotalTasksCompletionTimeInSeconds;
            }
            // divide the result with the amount of complete tasks
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

        public static int CalcTotalAmountOfUniqueUsers()
        {
            var allUserNames = new List<string>();

            // find all names and add to list 
            foreach(var tasks in CompleteTasks)
            {
                foreach(var names in tasks.IndividualTasks)
                {
                    allUserNames.Add(names.UserName);
                }
            }

            // find only unique names in list
            return allUserNames.Distinct().Count();
        }

        public static int CalcTotalAmountOfUI()
        {
            var allUI = new List<UserInteractions>();

            // find all ui and add to list 
            foreach (var tasks in CompleteTasks)
            {
                foreach (var ui in tasks.IndividualTasks)
                {
                    if (ui.Data.UserInteractions != UserInteractions.MANATEE)
                    {
                        allUI.Add(ui.Data.UserInteractions);
                    }
                }
            }

            // return arr count
            return allUI.Count;
        }
        
        public static int CalcTotalAmountOfSteps()
        {
            var allSteps = new List<string>();

            // find all individual tasks steps and add to list 
            foreach (var tasks in CompleteTasks)
            {
                foreach (var steps in tasks.IndividualTasks)
                {
                    allSteps.Add(steps.Data.Data);
                }
            }

            // return arr count
            return allSteps.Count;
        } 
        
        public static int CalcTotalAmountOfApps()
        {
            var allApps = new List<UserInteractions>();

            // find all apps that have been opened and add to list 
            foreach (var tasks in CompleteTasks)
            {
                foreach (var apps in tasks.IndividualTasks)
                {
                    if (apps.Data.UserInteractions.Equals(UserInteractions.WINDOW_OPEN))
                    {
                        Console.WriteLine(apps.Data.UserInteractions);
                        allApps.Add(apps.Data.UserInteractions);
                    }
                }
            }

            // return arr count
            return allApps.Count();
        }

        public static List<string> GetID()
        {
            return CompleteTasks
                .Select(data => data.CompleteTaskID)
                .ToList();
        }

        public static List<string> GetAllNames()
        {
            return CompleteTasks
                .Select(data => data.CompleteTaskName)
                .ToList();
        }

        public static Dictionary<string, int> GetTotalTasks()
        {
            var dic = new Dictionary<string, int>();

            var tasks = CompleteTasks
                .Select(data => new { data.CompleteTaskName, data.TotalIndividualTasks })
                .ToList();

            foreach (var task in tasks)
            {
                dic.Add(task.CompleteTaskName, task.TotalIndividualTasks);
            }
            return dic;
        }

        public static CompleteTask GetTask(string name)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task ?? throw new Exception($"{name} not found Exception");
        }

        public static CompleteTask GetTaskByID(string id)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task ?? throw new Exception($"No task corresponds with {id} exception");
        }

        public static int GetTaskDataFreq(string name, string data)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualTaskFrequency(data) :
                throw new Exception($"No task corresponds with {name} exception");
        }

        public static int GetTaskUserInteractionFreq(string name, string ui)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualUserInteractionsFrequency(ui) :
                throw new Exception($"No task corresponds with {name} exception");
        }

        public static int GetTaskDataFreqByID(string id, string data)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualTaskFrequency(data) :
                throw new Exception($"No task corresponds with {id} exception");
        }

        public static int GetTaskUserInteractionFreqByID(string id, string ui)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualUserInteractionsFrequency(ui) :
                throw new Exception($"No task corresponds with {id} exception");
        }
    }
}   
