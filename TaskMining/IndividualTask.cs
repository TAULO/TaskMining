using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class IndividualTask
    {
        // unix ts
        public string TimeStamp { get; set; }   
        public string MachineName { get; set; } 
        public string UserName { get; set; }    
        public string ApplicationName { get; set; }
        public IndividualTaskData Data { get; set; }
        public IndividualTask(string timeStamp, string machineName, string userName, string applicationName, IndividualTaskData data)
        {
            TimeStamp = timeStamp;
            MachineName = machineName;
            UserName = userName;
            ApplicationName = applicationName;
            Data = data;
        }  

        public DateTime GetDateTime()
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(double.Parse(TimeStamp) / 1000);
            return dateTime;
        }

        public DateTime GetDateTime(double ts)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(ts / 1000);
            return dateTime;
        }

        // calculate total time a task has been completed in seconds
        public double TotalTaskCompletionTimeInSeconds(IndividualTask wtStart, IndividualTask wtEnd)
        {
            double ts = double.Parse(wtEnd.TimeStamp) - double.Parse(wtStart.TimeStamp);
            return ts;
        }
        public DateTime TotalTaskCompletionTime(IndividualTask wtStart, IndividualTask wtEnd)
        {
            double subtractTs = TotalTaskCompletionTimeInSeconds(wtStart, wtEnd);
            long uxNow = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            
            DateTime time = GetDateTime(uxNow - subtractTs);

            Console.WriteLine(time);

            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }

        override
        public string ToString()
        {
            return $"Timestamp          Machine name          User name          Application name          Data User                                        User Interactions\n" +
                   $"{TimeStamp}      {MachineName}       {UserName}              {ApplicationName}                {Data.Data.Replace("{applicationName}", ApplicationName)}                                           {Data.UserInteractions}\n";
        }
    }
}
