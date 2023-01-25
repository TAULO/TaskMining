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
        /// <summary>
        /// The ID of the completed task.
        /// </summary>
        [JsonPropertyName("ID")]
        public string CompleteTaskID { get; }

        /// <summary>
        /// The name of the completed task.
        /// </summary>
        [JsonPropertyName("name")]
        public string CompleteTaskName { get; set; }

        /// <summary>
        /// The total amount of users at who have been involved in the task.
        /// </summary>
        [JsonPropertyName("userAmount")]
        public int TotalAmountOfUsers { get; }

        /// <summary>
        /// The username of user. (the user who started the task).
        /// </summary>
        [JsonPropertyName("userName")]
        public string IndividualTasksUserName { get => IndividualTasks[0].UserName; }

        /// <summary>
        /// The machine name.
        /// </summary>
        [JsonPropertyName("machineName")]
        public string IndividualTasksMachineName { get => IndividualTasks[0].MachineName; }

        /// <summary>
        /// The total amount of applications used in the task.
        /// </summary>
        [JsonPropertyName("applicationsUsed")]
        public int TotalCompleteTaskApplicationsUsed { get; }
        
        /// <summary>
        /// The total number of seconds spent pr appliation. 
        /// </summary>
        [JsonPropertyName("timeSpentPrApplication")]
        public Dictionary<string, double> TimeSpentPrApplication { get; }

        /// <summary>
        /// The total number of seconds spent at each individual task. 
        /// </summary>
        [JsonPropertyName("individualTaskTime")]
        public List<double> IndividualTaskTime { get; }

        /// <summary>
        /// Total amount of individual tasks. 
        /// </summary>
        [JsonPropertyName("tasksCount")]
        public int TotalIndividualTasks { get; }

        /// <summary>
        /// The total amount of user interactions. 
        /// </summary>
        [JsonPropertyName("userInteractionsCount")]
        public int TotalAmountOfUserInteractionActions { get; }
        
        /// <summary>
        /// A list of every individual task. 
        /// </summary>
        [JsonPropertyName("tasks")]
        public List<string> IndividualTaskDataList { get; }
       
        /// <summary>
        /// A list of every user interaction
        /// </summary>
        [JsonPropertyName("userInteractions")]
        public List<string> IndividualTaskUserInteractionsList { get; }
        
        // TODO: Fix
        [JsonPropertyName("taskCompletionTime")]
        public DateTime TotalTasksCompletionTime { get; }

        /// <summary>
        /// The number of seconds the completed task has taken to complete
        /// </summary>
        [JsonPropertyName("taskCompletionTimeSeconds")]
        public double TotalTasksCompletionTimeInSeconds { get; }

        /// <summary>
        /// The total amount of individual task one or multiple users has done.
        /// </summary>
        [JsonPropertyName("individualTaskUserCount")]
        public Dictionary<string, int> IndividualCountTaskPrUser { get; }

        /// <summary>
        /// The total amount of user interactions one or multiple users has done.
        /// </summary>
        [JsonPropertyName("individualTaskUserInteractionsCount")]
        public Dictionary<string, int> IndividualTaskUserInteractionsCount { get; }
        
    
        //public Dictionary<string, int> IndividualTaskDataCount { get; }

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
            IndividualTaskTime = CalcIndividualTaskTime();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
            IndividualCountTaskPrUser = CalcIndividualCountTaskPrUser();
            IndividualTaskUserInteractionsCount = CalcIndividualTaskUserInteractionsCount();
            //IndividualTaskDataCount = CalcIndividualTaskDataCount();
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
            IndividualTaskTime = CalcIndividualTaskTime();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
            IndividualCountTaskPrUser = CalcIndividualCountTaskPrUser();
            IndividualTaskUserInteractionsCount = CalcIndividualTaskUserInteractionsCount();
            //IndividualTaskDataCount = CalcIndividualTaskDataCount();
        }

        public CompleteTask(string completeTaskName, string csvPath)
        {
            HandleCSV(csvPath);
            CompleteTaskID = Guid.NewGuid().ToString("N");
            CompleteTaskName = completeTaskName;
            TotalIndividualTasks = IndividualTasks.Count;
            TotalAmountOfUsers = CalcTotalAmountOfUsers();
            IndividualTaskTime = CalcIndividualTaskTime();
            TotalCompleteTaskApplicationsUsed = CalcTotalCompleteTaskApplicationsUsed();
            TimeSpentPrApplication = CalcTimeSpentPrApplication();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
            IndividualCountTaskPrUser = CalcIndividualCountTaskPrUser();
            IndividualTaskUserInteractionsCount = CalcIndividualTaskUserInteractionsCount();
            //IndividualTaskDataCount = CalcIndividualTaskDataCount();
        }

        /// <summary>
        /// Adds the CSV data to IndividualTask class
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void HandleCSV(Stream data)
        {
            try
            {
                var csvParser = new TextFieldParser(data);

                // set specied value of ","
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // skip header line
                csvParser.ReadLine();

                // read every line in CSV file
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

        public void HandleCSV(string data)
        {
            try
            {
                var csvParser = new TextFieldParser(data);

                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // skip header line
                csvParser.ReadLine();

                // read every line in CSV-file
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

        private List<double> CalcIndividualTaskTime()
        {
            var list = new List<double>();
            for (int i = 0; i < IndividualTasks.Count; i++)
            {
                // if i equals the length of the array, then add 0 to list, because the individual task 
                // completion time cannot be calulated without a second task. 
                if (i == IndividualTasks.Count - 1)
                {
                    list.Add(0);
                }
                else
                {
                    list.Add(double.Parse(IndividualTasks[i + 1].TimeStamp) - double.Parse(IndividualTasks[i].TimeStamp));
                }
            }
            return list;
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

        // returns a dictionary that maps the application names to the total time spent using each application
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
            
            // for each element in the list, calculate the time spent using each application
            // by subtracting the start time from the end time
            for(int i = 0; i < taskStart.Count; i++)
            {
                double tsStart = double.Parse(taskStart[i].tsStart);
                double tsEnd = double.Parse(taskEnd[i].tsEnd);
                double res = tsEnd - tsStart;

                // if application name is not already a key 
                // updates the existing value by adding the time spent using the application.
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

        // returns the number of IndividualTasks that have a User Interactions of WINDOW OPEN or return 0
        private int CalcTotalCompleteTaskApplicationsUsed()
        {
            var tasks = IndividualTasks
                .GroupBy(task => task.Data.UserInteractions)
                .Select(x => new { Element = x.Key, Counter = x.Count() })
                .Where(task => task.Element.Equals(UserInteractions.WINDOW_OPEN))
                .FirstOrDefault();

            return tasks != null ? tasks.Counter : 0; 
        }

        // returns the number of user interactions that does not have user interactions of MANATEE or return 0
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

        // return the amount of users if none return 0
        private int CalcTotalAmountOfUsers()
        {
            return IndividualTasks
                .GroupBy(task => task.UserName)
                .Select(task => task.Distinct())
                .ToArray()
                .Length;
        }

        // return a dictionary that maps user names to the total number of individual tasks performed by each user
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

        // return a dictionary that maps User Interaction to the total number of User interaction performed by each User Interaction
        private Dictionary<string, int> CalcIndividualTaskUserInteractionsCount()
        {
            var dic = new Dictionary<string, int>();

            var task = IndividualTasks
                .GroupBy(task => task.Data.UserInteractions)
                .Select(task => new { Ui = task.Key, UiCount = task.Distinct().Count() })
                .ToList();

            foreach (var tsk in task)
            {
                dic.Add(tsk.Ui.ToString(), tsk.UiCount);
            }

            return dic;
        }
        private Dictionary<string, int> CalcIndividualTaskDataCount()
        {
            var dic = new Dictionary<string, int>();

            var task = IndividualTasks
                .GroupBy(task => task.Data.Data)
                .Select(task => new { Data = task.Key, DataCount = task.Distinct().Count() })
                .ToList();

            foreach (var tsk in task)
            {
                dic.Add(tsk.Data, tsk.DataCount);
            }

            return dic;
        }
    }
}
