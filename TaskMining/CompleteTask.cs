﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskMining
{
    public class CompleteTask : IHandleCSVData
    {
        [JsonPropertyName("ID")]
        public string CompleteTaskID { get; }

        [JsonPropertyName("name")]
        public string CompleteTaskName { get; set; }
        
        
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

        public readonly List<IndividualTask> IndividualTasks = new List<IndividualTask>();
        public CompleteTask(string individualTaskName, string taskDataPath)
        {
            HandleCSV(taskDataPath);
            CompleteTaskID = Guid.NewGuid().ToString("N");
            CompleteTaskName = individualTaskName;
            TotalIndividualTasks = IndividualTasks.Count;
            TotalCompleteTaskApplicationsUsed = CalcTotalCompleteTaskApplicationsUsed();
            TimeSpentPrApplication = CalcTimeSpentPrApplication();
            TotalAmountOfUserInteractionActions = CalcTotalAmountOfUserInteractionActions();
            IndividualTaskDataList = GetIndividualTaskData();
            IndividualTaskUserInteractionsList = GetIndividualTaskUserInteractions();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(double.Parse(IndividualTasks[0].TimeStamp), double.Parse(IndividualTasks[^1].TimeStamp));
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
        }
        public void HandleCSV(string path)
        {
            try
            {
                var csvParser = new TextFieldParser(path);

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

                        IndividualTaskData data = new IndividualTaskData(fields[4], IndividualTaskData.GetUserInteractions(fields[5]));

                        IndividualTasks.Add(new IndividualTask(timeStamp, machineName, userName, applicationName, data));
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
            
            return tasks != null ? tasks.Counter : throw new Exception($"{taskData} an exception msg"); // make exception;
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

            return tasks != null ? tasks.Counter : throw new Exception("an exception msg"); // make exception
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

            return tasks != null ? tasks.Counter : throw new Exception("an exception msg"); // make exception; 
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

        public bool ArrayIsEqual(List<IndividualTask> secondList)
        {
            return IndividualTasks.Count != secondList.Count ? false : IndividualTasks.SequenceEqual(secondList);
        }
    }
}
