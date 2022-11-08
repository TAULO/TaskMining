using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public class IndividualTask
    {
        // unix ts
        public string TimeStamp { get; }   
        public string MachineName { get; } 
        public string UserName { get; }    
        public string ApplicationName { get; }
        public IndividualTaskData Data { get; }
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
            return dateTime.AddSeconds(double.Parse(TimeStamp) / 1000);
        }

        override
        public string ToString()
        {
            return $"Timestamp\t\tMachine name\t\tUser name\t\tApplication name\t\tData User\t\tUser Interactions\n" +
                   $"{TimeStamp}\t\t{MachineName}\t\t{UserName}\t\t{ApplicationName}\t\t{Data.Data.Replace("{applicationName}", ApplicationName)}                                           {Data.UserInteractions}\n";
        }
    }
}
