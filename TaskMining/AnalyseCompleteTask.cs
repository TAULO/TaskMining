using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public static class AnalyseCompleteTask
    {

        public static List<CompleteTask> CompleteTasks = new List<CompleteTask>();

        public static int TotalCompleteTasks { get => CompleteTasks.Count; }

        // TODO: Finish this function
        public static List<string> RepeatableUserInteracions(CompleteTask currCompleteTask)
        {
            var found = CompleteTasks.Find(task => task.CompleteTaskName.Equals(currCompleteTask.CompleteTaskName));

            var repeatableTasksList = new List<string>();

            foreach(var task in CompleteTasks)
            {
                if (found == null) 
                    return repeatableTasksList;
                
                var uiList = task.IndividualTasks.Select(ui => ui.Data.UserInteractions).ToList();
                var currList = found.IndividualTasks.Select(ui => ui.Data.UserInteractions).ToList();
                bool isRepeatable = uiList.SequenceEqual(currList);

                Console.WriteLine(isRepeatable);
                if(isRepeatable)
                {
                    repeatableTasksList.Add(task.CompleteTaskName);
                }
            }
            return repeatableTasksList;
        }

        // TODO: Finish this function
        public static List<CompleteTask> RepeatableIndividualTasks(CompleteTask currCompleteTask)
        {

            var repeatableTasksList = new List<CompleteTask>();

            foreach (var task in CompleteTasks)
            {
                var uiList = task.IndividualTasks.Select(ui => ui.Data.Data).ToList();
                var currList = currCompleteTask.IndividualTasks.Select(ui => ui.Data.Data).ToList();
                bool isRepeatable = uiList.SequenceEqual(currList);

                Console.WriteLine(isRepeatable);
                if (isRepeatable)
                {
                    repeatableTasksList.Add(task);
                }
            }
            return repeatableTasksList;
        }

        //TODO: Finish this function
        public static List<CompleteTask> ReapeatableCompleteTasks(CompleteTask currCompleteTask)
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
        /// <returns>The exact number of the specified actionsIf specified action not found, returns 0</returns>
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

        /// <summary>
        /// Gets the total amount of times a single individual task has occurred. 
        /// </summary>
        /// <returns>A dictionary, where the key is the individual task and the value is the number of times the individual task has occurred.</returns>
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
                    var key = dic.Key.Data.Data;
                    var value = dic.Value.Counter;
                    if (!result.ContainsKey(key))
                    {
                        result.Add(key, value);
                    }
                    else
                    {
                        result[key] += value;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the total amount of times a user interaction has occurred.
        /// </summary>
        /// <returns>A dictionary, where the key is the user interaction and the value is the number of times the user interaction has occurred.</returns>
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
                    var key = dic.Key.Element;
                    var value = dic.Value.Counter;
                    if (!result.ContainsKey(key))
                    {
                        result.Add(key, value);
                    }
                    else
                    {
                        result[key] += value;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the number of seconds each completed task has taken to complete.
        /// </summary>
        /// <returns>A dictionary, where the key is the name of the task and the value is the number of seconds the completed task has taken to complete.</returns>
        public static Dictionary<string, double> CompletionTimePrCompleteTask()
        {
            var dic = new Dictionary<string, double>();

            foreach(var task in CompleteTasks)
            {
                dic.Add(task.CompleteTaskName, task.TotalTasksCompletionTimeInSeconds);
            }
            return dic; 
        }

        /// <summary>
        /// Gets the average time the complete tasks has taken to complete.
        /// </summary>
        /// <returns>The average completion time. If none returns 0.</returns>
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
        // fix nesting
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

        /// <summary>
        /// Gets the total amount of unique users. 
        /// </summary>
        /// <returns>The total amount of unique users. If none returns 0.</returns>
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

        /// <summary>
        /// Gets the total amount of user interactions. 
        /// </summary>
        /// <returns>The total amount of user interactions that is not equal to MANATEE. If none returns 0.</returns>
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
        
        /// <summary>
        /// Gets the total amount of user interactions. 
        /// </summary>
        /// <returns>The total amount of user interactions. If none returns 0.</returns>
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
        
        /// <summary>
        /// Gets the total amount of applications used.
        /// </summary>
        /// <returns>The total amount of applications used. If none returns 0.</returns>
        public static int CalcTotalAmountOfApps()
        {
            var allApps = new List<string>();

            // find all apps that have been opened and add to list 
            foreach (var tasks in CompleteTasks)
            {
                foreach (var apps in tasks.IndividualTasks)
                {
                    if (apps.Data.UserInteractions.Equals(UserInteractions.WINDOW_OPEN))
                    {
                        allApps.Add(apps.ApplicationName);
                    }
                }
            }

            // find only unique apps 
            return allApps.Distinct().Count();
        }

        /// <summary>
        /// Gets all the completed tasks ID.
        /// </summary>
        /// <returns>A list of all the completed tasks ID.</returns>
        public static List<string> GetID()
        {
            return CompleteTasks
                .Select(data => data.CompleteTaskID)
                .ToList();
        }

        /// <summary>
        /// Gets all the completed tasks name.
        /// </summary>
        /// <returns>A list of the names of all the completed tasks.</returns>
        public static List<string> GetAllNames()
        {
            return CompleteTasks
                .Select(data => data.CompleteTaskName)
                .ToList();
        }

        /// <summary>
        /// Gets the total amount of individual tasks that has been occurred. 
        /// </summary>
        /// <returns>A dictionary, where the key is the name of the completed tasks and the value is the amount of indiduval tasks that has been occurred.</returns>
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

        /// <summary>
        /// Get the task by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A Complete Task specified by the name</returns>
        /// <exception cref="Exception"></exception>
        public static CompleteTask GetTask(string name)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task ?? throw new Exception($"{name} not found Exception");
        }

        /// <summary>
        /// Get the task by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Complete Task specified by the ID.</returns>
        /// <exception cref="Exception"></exception>
        public static CompleteTask GetTaskByID(string id)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task ?? throw new Exception($"No task corresponds with {id} exception");
        }

        /// <summary>
        /// Gets the total amount of data frequency by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns>The total amount of data frequency specified by the name of the completed task and the name of the individual task.</returns>
        /// <exception cref="Exception"></exception>
        public static int GetTaskDataFreq(string name, string data)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualTaskFrequency(data) :
                throw new Exception($"No task corresponds with {name} exception");
        }

        /// <summary>
        /// Gets the total amount of user interactions frequency by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ui"></param>
        /// <returns>The total amount of user interactions specified by the name of completed task and the name of the user interaction.</returns>
        /// <exception cref="Exception"></exception>
        public static int GetTaskUserInteractionFreq(string name, string ui)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualUserInteractionsFrequency(ui) :
                throw new Exception($"No task corresponds with {name} exception");
        }

        /// <summary>
        /// Gets the total amount of data frequency by ID.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns>The total amount of data frequency specified by the ID of the completed task and the name of the individual task.</returns>
        /// <exception cref="Exception"></exception>
        public static int GetTaskDataFreqByID(string id, string data)
        {
            var task = CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualTaskFrequency(data) :
                throw new Exception($"No task corresponds with {id} exception");
        }

        /// <summary>
        /// Gets the total amount of user interactions frequency by ID.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ui"></param>
        /// <returns>The total amount of user interactions specified by the ID of completed task and the name of the user interaction.</returns>
        /// <exception cref="Exception"></exception>
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
