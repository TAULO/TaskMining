﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class IndividualTask
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
            return $"Timestamp          Machine name          User name          Application name          Data User                                        User Interactions\n" +
                   $"{TimeStamp}      {MachineName}       {UserName}              {ApplicationName}                {Data.Data.Replace("{applicationName}", ApplicationName)}                                           {Data.UserInteractions}\n";
        }
    }
}
