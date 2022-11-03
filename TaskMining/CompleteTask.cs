using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class CompleteTask : IHandleCSVData
    {
        public string CompleteTaskID { get; }
        public string CompleteTaskName { get; set; }
        public int TotalIndividualTasks { get; }
        public int TotalCompleteTaskApplicationsUsed { get; }
        public DateTime TotalTasksCompletionTime { get; }
        public double TotalTasksCompletionTimeInSeconds { get; }

        public readonly List<IndividualTask> IndividualTasks = new List<IndividualTask>();
        public CompleteTask(string individualTaskName, string taskDataPath)
        {
            HandleCSV(taskDataPath);
            CompleteTaskID = Guid.NewGuid().ToString("N");
            CompleteTaskName = individualTaskName;
            TotalIndividualTasks = IndividualTasks.Count;
            TotalCompleteTaskApplicationsUsed = CalcTotalCompleteTaskApplicationsUsed();
            TotalTasksCompletionTimeInSeconds = TotalTaskCompletionTimeInSeconds(IndividualTasks[0], IndividualTasks[^1]);
            TotalTasksCompletionTime = TotalTaskCompletionTime(IndividualTasks[0], IndividualTasks[^1]);
        }

        public static DateTime GetDateTime(double ts)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(ts / 1000);
        }

        // Calculate the total time a complete task has been completed from app load to app close in seconds
        private double TotalTaskCompletionTimeInSeconds(IndividualTask wtStart, IndividualTask wtEnd)
        {
            if (IndividualTasks.Count <= 1) throw new Exception("Not enough tasks exception"); // make exception to throw

            return double.Parse(wtEnd.TimeStamp) - double.Parse(wtStart.TimeStamp);
        }

        // does not work fix fix fix
        private DateTime TotalTaskCompletionTime(IndividualTask wtStart, IndividualTask wtEnd)
        {
            if (IndividualTasks.Count <= 1) throw new Exception("Not enough tasks exception"); // make exception to throw
            
            double subtractTs = TotalTaskCompletionTimeInSeconds(wtStart, wtEnd);
            long uxNow = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            DateTime time = GetDateTime(uxNow - subtractTs);
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }

        public void HandleCSV(string path)
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

        /// <summary>
        /// Returns the total amount the same task has occurred.     
        /// </summary>
        /// <param name="taskData">Task data to search for</param>
        /// <returns></returns>
        public int IndividualTaskFrequency(string taskData)
        {
            var arr = IndividualTasks
               .GroupBy(task => task.Data.Data)
               .Select(y => new { Element = y.Key, Counter = y.Count() })
               .Where(task => task.Element.ToLower().Equals(taskData.ToLower()))
               .FirstOrDefault();
            
            return arr != null ? arr.Counter : throw new Exception("an exception msg"); // make exception;
        }

        /// <summary>
        /// Returns the total amount a user interaction has occurred.
        /// </summary>
        /// <param name="userInteraction"></param>
        /// <returns></returns>
        public int IndividualUserInteractionsFrequency(string userInteraction)
        {
            var arr = IndividualTasks
                .GroupBy(task => task.Data.UserInteractions)
                .Select(y => new { Element = y.Key, Counter = y.Count() })
                .Where(task => task.Element.ToString().Equals(userInteraction))
                .FirstOrDefault();

            return arr != null ? arr.Counter : throw new Exception("an exception msg"); // make exception
        }

        private int CalcTotalCompleteTaskApplicationsUsed()
        {
            var arr = IndividualTasks
                .GroupBy(task => task.Data.UserInteractions)
                .Select(x => new { Element = x.Key, Counter = x.Count() })
                .Where(task => task.Element.Equals(UserInteractions.WINDOW_OPEN))
                .FirstOrDefault();

            return arr != null ? arr.Counter : throw new Exception("an exception msg"); // make exception; 
        }
    }
}
