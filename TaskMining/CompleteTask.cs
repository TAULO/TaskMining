using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TaskMining
{
    public class CompleteTask 
    {
        [JsonPropertyName("ID")]
        public string CompleteTaskID { get; }

        [JsonPropertyName("name")]
        public string CompleteTaskName { get; set; }

        [JsonPropertyName("userAmount")]
        public int TotalAmountOfUsers { get; }

        [JsonPropertyName("userName")]
        public string IndividualTasksUserName { get => IndividualTasks[0].UserName; }

        [JsonPropertyName("machineName")]
        public string IndividualTasksMachineName { get => IndividualTasks[0].MachineName; }

        [JsonPropertyName("applicationsUsed")]
        public int TotalCompleteTaskApplicationsUsed { get; }
        
        [JsonPropertyName("timeSpentPrApplication")]
        public Dictionary<string, double> TimeSpentPrApplication { get; }
        
        [JsonPropertyName("tasksCount")]
        public int TotalIndividualTasks { get; }
        
        [JsonPropertyName("userInteractionsCount")]
        public int TotalAmountOfUserInteractionActions { get; }
        
        [JsonPropertyName("tasks")]
        public List<string> IndividualTaskDataList { get; }
       
        [JsonPropertyName("userInteractions")]
        public List<string> IndividualTaskUserInteractionsList { get; }
        
        [JsonPropertyName("taskCompletionTime")]
        public DateTime TotalTasksCompletionTime { get; }
        
        [JsonPropertyName("taskCompletionTimeSeconds")]
        public double TotalTasksCompletionTimeInSeconds { get; }

        [JsonPropertyName("individualTaskUserCount")]
        public Dictionary<string, int> IndividualCountTaskPrUser { get; }

        public readonly List<IndividualTask> IndividualTasks = new List<IndividualTask>();
        public CompleteTask(string completeTaskName, Stream dataStream)
        {
            HandleCSV(dataStream);
            CompleteTaskID = Guid.NewGuid().ToString("N");
            CompleteTaskName = completeTaskName;
            TotalIndividualTasks = IndividualTasks.Count;
            TotalAmountOfUsers = CalcTotalAmountOfUsers();
            TotalCompleteTaskApplicationsUsed = CalcTotalCompleteTaskApplicationsUsed();
            TimeSpentPrApplication = CalcTimeSpentPrApplication();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
            IndividualCountTaskPrUser = CalcIndividualCountTaskPrUser();
        }

        public CompleteTask(string id, string completeTaskName, Stream dataStream)
        {
            HandleCSV(dataStream);
            CompleteTaskID = id;
            CompleteTaskName = completeTaskName;
            TotalIndividualTasks = IndividualTasks.Count;
            TotalAmountOfUsers = CalcTotalAmountOfUsers();
            TotalCompleteTaskApplicationsUsed = CalcTotalCompleteTaskApplicationsUsed();
            TimeSpentPrApplication = CalcTimeSpentPrApplication();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
            IndividualCountTaskPrUser = CalcIndividualCountTaskPrUser();
        }

        // for testing
        public CompleteTask(string completeTaskName, string csvPath)
        {
            HandleCSV(csvPath);
            CompleteTaskID = Guid.NewGuid().ToString("N");
            CompleteTaskName = completeTaskName;
            TotalIndividualTasks = IndividualTasks.Count;
            TotalAmountOfUsers = CalcTotalAmountOfUsers();
            TotalCompleteTaskApplicationsUsed = CalcTotalCompleteTaskApplicationsUsed();
            TimeSpentPrApplication = CalcTimeSpentPrApplication();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
            IndividualCountTaskPrUser = CalcIndividualCountTaskPrUser();
        }
        public void HandleCSV(Stream data)
        {
            try
            {
               //make check if path is .csv
                var csvParser = new TextFieldParser(data);

                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // skip header line
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[]? fields = csvParser.ReadFields();

                    if (fields == null)
                    {
                        throw new ArgumentNullException(nameof(fields));
                    }
                    else
                    {
                        string timeStamp = fields[0];
                        string machineName = fields[1];
                        string userName = fields[2];
                        string applicationName = fields[3];
                        IndividualTaskData indData = new IndividualTaskData(fields[4], IndividualTaskData.GetUserInteractions(fields[5]));

                        IndividualTasks.Add(new IndividualTask(timeStamp, machineName, userName, applicationName, indData));
                    }
                }
                csvParser.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // for testing
        public void HandleCSV(string data)
        {
            try
            {
                //make check if path is .csv
                var csvParser = new TextFieldParser(data);

                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // skip header line
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[]? fields = csvParser.ReadFields();

                    if (fields == null)
                    {
                        throw new ArgumentNullException(nameof(fields));
                    }
                    else
                    {
                        string timeStamp = fields[0];
                        string machineName = fields[1];
                        string userName = fields[2];
                        string applicationName = fields[3];
                        IndividualTaskData indData = new IndividualTaskData(fields[4], IndividualTaskData.GetUserInteractions(fields[5]));

                        IndividualTasks.Add(new IndividualTask(timeStamp, machineName, userName, applicationName, indData));
                    }
                }
                csvParser.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private DateTime GetDateTimeHelper(double ts)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(ts / 1000);
        }

        // Calculate the total time a complete task has been completed from app load to app close in seconds
        private double TotalTaskCompletionTimeInSeconds(IndividualTask wtStart, IndividualTask wtEnd)
        {
            return IndividualTasks.Count < 2 ? throw new Exception("Not enough tasks exception") : double.Parse(wtEnd.TimeStamp) - double.Parse(wtStart.TimeStamp);
        }

        private double TotalTaskCompletionTimeInSeconds(double wtStart, double wtEnd)
        {
            return IndividualTasks.Count < 2 ? throw new Exception("Not enough tasks exception") : wtEnd - wtStart;
        }

        // does not work, maybe fix or delete if useless
        private DateTime TotalTaskCompletionTime(IndividualTask wtStart, IndividualTask wtEnd)
        {
            if (IndividualTasks.Count <= 1) throw new Exception("Not enough tasks exception"); // make exception to throw
            
            double subtractTs = TotalTaskCompletionTimeInSeconds(wtStart, wtEnd);
            long uxNow = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            double res = uxNow - subtractTs;
            DateTime time = GetDateTimeHelper(res);

            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }

        /// <summary>
        /// Gets the total amount a action has occurred in a complete task.     
        /// </summary>
        /// <param name="taskData">Task data to search for</param>
        /// <returns></returns>
        public int IndividualTaskFrequency(string taskData)
        {
            var tasks = IndividualTasks
               .GroupBy(task => task.Data.Data)
               .Select(x => new { Element = x.Key, Counter = x.Count() })
               .Where(task => task.Element.ToLower().Equals(taskData.ToLower()))
               .FirstOrDefault();

            return tasks != null ? tasks.Counter : 0;
        }

        /// <summary>
        /// Gets the total amount a user interaction has occurred in a complete task.
        /// </summary>
        /// <param name="userInteraction"></param>
        /// <returns></returns>
        public int IndividualUserInteractionsFrequency(object userInteraction)
        {
            if (userInteraction.GetType() != typeof(string) && userInteraction.GetType() != typeof(UserInteractions))
                throw new Exception("wrong type exception msg"); // make exception

            var tasks = IndividualTasks
                .GroupBy(task => task.Data.UserInteractions)
                .Select(x => new { Element = x.Key, Counter = x.Count() })
                .Where(task => task.Element.ToString().Equals(userInteraction) || task.Element.Equals(userInteraction))
                .FirstOrDefault();

            return tasks != null ? tasks.Counter : 0;
        }

        private List<string> GetIndividualTaskData()
        {
            return IndividualTasks
                .Select(task => task.Data.Data)
                .ToList();
        }

        private List<string> GetIndividualTaskUserInteractions()
        {
            return IndividualTasks
                .Select(task => task.Data.UserInteractions.ToString())
                .ToList();
        }

        private Dictionary<string, double> CalcTimeSpentPrApplication()
        {
            var dic = new Dictionary<string, double>();

            // the timestamp when the window is focused
            var taskStart = IndividualTasks
                .GroupBy(task => new { task.TimeStamp, task.ApplicationName, task.Data.UserInteractions })
                .Select(ts => ts.Key)
                .Where(ui => ui.UserInteractions.Equals(UserInteractions.WINDOW_FOCUS))
                .Select(task => new { task.ApplicationName, tsStart = task.TimeStamp })
                .ToList();

            // the timestmap when the window is unfocused
            var taskEnd = IndividualTasks
                .GroupBy(task => new { task.TimeStamp, task.ApplicationName, task.Data.UserInteractions })
                .Select(ts => ts.Key)
                .Where(ui => ui.UserInteractions.Equals(UserInteractions.WINDOW_UNFOCUS))
                .Select(task => new { task.ApplicationName, tsEnd = task.TimeStamp })
                .ToList();

            for(int i = 0; i < taskStart.Count; i++)
            {
                double tsStart = double.Parse(taskStart[i].tsStart);
                double tsEnd = double.Parse(taskEnd[i].tsEnd);
                double res = tsEnd - tsStart;

                if (!dic.ContainsKey(taskStart[i].ApplicationName))
                {
                    dic.Add(taskStart[i].ApplicationName, res);
                } else
                {
                    dic[taskStart[i].ApplicationName] += res;
                }
            }
            return dic;
        }

        private int CalcTotalCompleteTaskApplicationsUsed()
        {
            var tasks = IndividualTasks
                .GroupBy(task => task.Data.UserInteractions)
                .Select(x => new { Element = x.Key, Counter = x.Count() })
                .Where(task => task.Element.Equals(UserInteractions.WINDOW_OPEN))
                .FirstOrDefault();

            return tasks != null ? tasks.Counter : 0; 
        }

        private int CalcTotalAmountOfUserInteractionActions()
        {
            int result = 0;
            IndividualTasks
                .GroupBy(task => new { task.Data.UserInteractions })
                .Select(ui => new { Element = ui, Counter = ui.Count() })
                .Where(ui => !ui.Element.Key.UserInteractions.Equals(UserInteractions.MANATEE))
                .ToList()
                .ForEach(t => result += t.Counter);

            return result;
        }

        private int CalcTotalAmountOfUsers()
        {
            return IndividualTasks
                .GroupBy(task => task.UserName)
                .Select(task => task.Distinct())
                .ToArray()
                .Length;
        }

        private Dictionary<string, int> CalcIndividualCountTaskPrUser()
        {
            var dic = new Dictionary<string, int>();

            var tasks = IndividualTasks
                .GroupBy(task => task.UserName)
                .Select(task => new { Name = task.Key, Dis = task.Distinct().Count()  })
                .ToArray();

            for (int i = 0; i < tasks.Length; i++)
            {
                dic.Add(tasks[i].Name, tasks[i].Dis);
            }
            return dic;
        }

        public bool ArrayIsEqual(List<IndividualTask> secondList)
        {
            return IndividualTasks.Count != secondList.Count ? false : IndividualTasks.SequenceEqual(secondList);
        }
    }
}
